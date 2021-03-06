using System;
using System.Linq;
using NuGet.Common;
using Nuke.Common;
using Nuke.Common.Execution;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.GitVersion;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

[CheckBuildProjectConfigurations]
[UnsetVisualStudioEnvironmentVariables]
class Build : NukeBuild
{
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode

    public static int Main () => Execute<Build>(x => x.Pack);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Solution] readonly Solution Solution;
    [GitRepository] readonly GitRepository GitRepository;
     [GitVersion] readonly GitVersion GitVersion;

    AbsolutePath SourceDirectory = RootDirectory / "MyPonto.Client";
    AbsolutePath TestsDirectory => Solution.Directory / "tests";
    AbsolutePath ArtifactsDirectory => RootDirectory / "artifacts";

    String[] TestProjects
    {
        get { return GlobFiles(TestsDirectory / "MyPonto.Tests", "*.Tests.csproj").ToArray(); }
    }
    
    [Parameter("NuGet Api Key",Name = "NUGET_API_KEY")] readonly string NUGET_API_KEY;
    [Parameter("NuGet Endpoint for Packages")] readonly string NUGET_ENDPOINT = "https://api.nuget.org/v3/index.json";

    private readonly string NugetProjectUrl = "https://github.com/Tieno/MyPonto.Client";

    [Parameter("MyPonto ClientId", Name = "MYPONTO_CLIENTID")] readonly string MYPONTO_CLIENTID;
    [Parameter("MyPonto ClientSecret", Name = "MYPONTO_CLIENTSECRET")] readonly string MYPONTO_CLIENTSECRET;

    Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {
            TestsDirectory.GlobDirectories("**/bin", "**/obj").ForEach(DeleteDirectory);
            EnsureCleanDirectory(ArtifactsDirectory);
        });

    Target Restore => _ => _
        .Executes(() =>
        {
            DotNetRestore(_ => _
                .SetProjectFile(Solution));
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            var test = GitVersion.AssemblySemVer;
            DotNetBuild(_ => _
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .SetAssemblyVersion(GitVersion.AssemblySemVer)
                .SetFileVersion(GitVersion.AssemblySemFileVer)
                .SetInformationalVersion(GitVersion.InformationalVersion)

                .EnableNoRestore());
        });
    Target Pack => _ => _
        .DependsOn(Clean)
        .DependsOn(Compile)
        .Executes(() =>
        {
            DotNetPack(s => s
                .SetOutputDirectory(ArtifactsDirectory)
                .SetProject(RootDirectory / "MyPonto.Client" / "MyPonto.Client.csproj")
                .SetPackageProjectUrl(NugetProjectUrl)
                //.SetIncludeSymbols(true)
                .SetLogOutput(true)
                .SetVersion(GitVersion.NuGetVersionV2)
                //
            );

        });

    public bool CanPublishNuget()
    {
        Logger.Info($"CanPublishNuget if on develop or master branch");
        Logger.Info($"GitRepo => {GitRepository.Branch}");
        if (GitRepository.IsOnDevelopBranch())
        {
            return true;
        }
        if (GitRepository.IsOnMasterBranch())
        {
            return true;
        }
        switch (GitRepository.Branch)
        {
            case "refs/heads/develop":
            case "refs/heads/master":
                return true;
            default:
                return false;
        }
    }
    Target Publish => _ => _
        .OnlyWhenDynamic(() => CanPublishNuget())
        .DependsOn(Test)
        .DependsOn(Pack)
        .Requires(() => NUGET_API_KEY)
        .Requires(() => NUGET_ENDPOINT)
        .Executes(() =>
   {


       var packages = ArtifactsDirectory.GlobFiles("*.nupkg");
       //add minor change
       DotNetNuGetPush(_ => _
               .SetSource(NUGET_ENDPOINT)
               .SetApiKey(NUGET_API_KEY)
               .SetLogOutput(true)
               .CombineWith(
                   packages, (_, v) => _
                       .SetTargetPath(v)),
           degreeOfParallelism: 5,
           completeOnFailure: true);


   });
    Target Test => _ => _
        .OnlyWhenStatic(() => TestProjects.Length > 0)
        .DependsOn(Compile)
        .Requires(() => MYPONTO_CLIENTID)
        .Requires(() => MYPONTO_CLIENTSECRET)
        .Executes(() =>
        {
            foreach (var testProject in TestProjects)
            {
                DotNetTest(s => s.SetProjectFile(testProject).SetFilter("Category!=RunLocal").SetLogOutput(true)
                    .SetListTests(true).SetVerbosity(DotNetVerbosity.Normal)
                    
                    .SetEnvironmentVariable(nameof(MYPONTO_CLIENTID), MYPONTO_CLIENTID)
                    .SetEnvironmentVariable(nameof(MYPONTO_CLIENTSECRET), MYPONTO_CLIENTSECRET)
                );
            }
          
        });



}
