using System;

using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using MyPonto.Client.Service;
using Xunit;

namespace MyPonto.Tests
{
    public class IntegrationTests
    {
        private IMyPontoService client;

        public IntegrationTests()
        {
            var builder = new ConfigurationBuilder().AddUserSecrets(Assembly.GetExecutingAssembly());
            Configuration = builder.Build();
            this.ClientId = Configuration["MYPONTO_CLIENTID"];
            this.ClientSecret = Configuration["MYPONTO_CLIENTSECRET"];
            this.client = MyPontoService.Create(ClientId, ClientSecret);
        }

        public IConfiguration Configuration { get; set; }

        private string? ClientSecret { get; set; }

        private string? ClientId { get; set; }

        [Fact]
        public async Task GetsAllAccounts_ResponseIsNotEmpty()
        {

            var response = await this.client.GetAccounts();

            response.Data.Should().NotBeNullOrEmpty("we expect at least 1 account");
           
        }
    }
}
