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
using static Nuke.Common.EnvironmentInfo;
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

    public static int Main () => Execute<Build>(x => x.Test);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Solution] readonly Solution Solution;
    [GitRepository] readonly GitRepository GitRepository;
     [GitVersion] readonly GitVersion GitVersion;

    AbsolutePath SourceDirectory = RootDirectory / "MyPonto.Client";
    AbsolutePath TestsDirectory => RootDirectory / "tests";
    AbsolutePath ArtifactsDirectory => RootDirectory / "artifacts";
    
    [Parameter("NuGet Api Key",Name = "NUGET_API_KEY")] readonly string NUGET_API_KEY;
    [Parameter("NuGet Endpoint for Packages")] readonly string NUGET_ENDPOINT = "https://api.nuget.org/v3/index.json";

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
        .DependsOn(Compile)
        .Executes(() =>
        {
            DotNetPack(s => s
                .SetOutputDirectory(ArtifactsDirectory)
                .SetProject(RootDirectory / "MyPonto.Client" / "MyPonto.Client.csproj")
                .SetVersion(GitVersion.NuGetVersionV2)
                //
            );

        });
    Target Publish => _ => _
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
                    .CombineWith(
                        packages, (_, v) => _
                            .SetTargetPath(v)),
                degreeOfParallelism: 5,
                completeOnFailure: true);

        });
    Target Test => _ => _
        .DependsOn(Compile)
        .Requires(() => MYPONTO_CLIENTID)
        .Requires(() => MYPONTO_CLIENTSECRET)
        .Executes(() =>
        {
            var testProjects = GlobFiles(TestsDirectory, "*\\*tests.csproj");
            var testRun = 1;
            foreach (var testProject in testProjects)
            {
                DotNetTest(s => s.SetProjectFile(testProject).SetFilter("Category!=RunLocal").SetLogOutput(true)
                    .SetListTests(true).SetNoBuild(true)
                    .SetEnvironmentVariable(nameof(MYPONTO_CLIENTID),MYPONTO_CLIENTID)
                    .SetEnvironmentVariable(nameof(MYPONTO_CLIENTSECRET), MYPONTO_CLIENTSECRET)
                );
            }
        });

}
