using System;

using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Tieno.MyPonto.Client.Service;
using Xunit;

namespace MyPonto.Tests
{
    
    public class IntegrationTests
    {
        private IMyPontoApi client;

        public IntegrationTests()
        {
            var builder = new ConfigurationBuilder().AddUserSecrets(Assembly.GetExecutingAssembly());
            Configuration = builder.Build();
            this.ClientId = Configuration["MYPONTO_CLIENTID"];
            this.ClientSecret = Configuration["MYPONTO_CLIENTSECRET"];
            this._pageSize = 5; 
            this.client = MyPontoApi.Create(ClientId, ClientSecret,_pageSize);
        }

        private readonly int _pageSize;

        public IConfiguration Configuration { get; set; }

        private string? ClientSecret { get; set; }

        private string? ClientId { get; set; }

        [Fact]
        public async Task GetsAllAccounts_ResponseIsNotEmpty()
        {
            var response = await this.client.Accounts.GetAccounts();
            response.Data.Should().NotBeNullOrEmpty("we expect at least 1 account");
            response.Data.Count.Should().BeGreaterThan(1);
        }
        [Fact]
        public async Task FetchingTransactions_ShouldEqualPageSize()
        {
            var account = (await this.client.Accounts.GetAccounts()).Data.Last();
            var transactions = await client.Transactions.GetTransactions(account.Id);
            transactions.Data.Count.Should().Be(_pageSize, "when fetching 5, we expect 5");
        }

        [Fact]
        public async Task GetTransactionsForAccount_ResponseIsNotEmpty()
        {
            var account = (await this.client.Accounts.GetAccounts()).Data.Last();
            var transactions = await client.Transactions.GetTransactions(account.Id);
            transactions.Data.Should().NotBeNullOrEmpty();

            transactions.Data.Count.Should().BeGreaterThan(3);
        }

        [Fact]
        public async Task GetAllTransactions_ReturnsMoreTransactionsThanPageSize()
        {
            var account = (await this.client.Accounts.GetAccounts()).Data.Last();
            var transactions = await client.Transactions.GetAllTransactions(account.Id);
            transactions.Data.Should().NotBeNullOrEmpty();

            transactions.Data.Count.Should().BeGreaterThan(_pageSize);
        }
        [Fact]
        public async Task GetTransactionsAfterLastTransaction_ReturnsEmptyResponse()
        {
            var account = (await this.client.Accounts.GetAccounts()).Data.Last();
            var lastTransaction = (await client.Transactions.GetTransactions(account.Id)).Data.First();
            var response = await client.Transactions.GetNewTransactions(account.Id, lastTransaction.Id);
            response.Data.Should().BeEmpty();
        }
        [Fact]
        public async Task TransactionExecutionDate_IsUtcTimeZone()
        {
            var account = (await this.client.Accounts.GetAccounts()).Data.First();
            var response = await client.Transactions.GetTransactions(account.Id);

            foreach (var transactionResource in response.Data)
            {
                transactionResource.Attributes.ExecutionDate.Kind.Should().Be(DateTimeKind.Utc);
                transactionResource.Attributes.ValueDate.Kind.Should().Be(DateTimeKind.Utc);
                transactionResource.Attributes.ExecutionDate.TimeOfDay.Should().Be(new TimeSpan(0));
                transactionResource.Attributes.ValueDate.TimeOfDay.Should().Be(new TimeSpan(0));

            }
        }


    }
}
