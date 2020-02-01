using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using MyPonto.Client.Model;
using Newtonsoft.Json;
using OAuth2ClientHandler;
using OAuth2ClientHandler.Authorizer;
using Timer = System.Timers.Timer;

namespace MyPonto.Client.Service
{
    public class MyPontoService : IMyPontoService
    {
        private readonly HttpClient _client;
        private readonly int _pageSize;

        public MyPontoService(HttpClient client)
        {
            _client = client;
            _pageSize = 20;
            
        }
        public MyPontoService(HttpClient client, int pageSize)
        {
            _client = client;
            _pageSize = pageSize;
        }

        public static MyPontoService Create(string clientId, string clientSecret, string pontoEndpoint = "https://api.myponto.com")
        {
            var options = new OAuthHttpHandlerOptions
            {
                AuthorizerOptions = new AuthorizerOptions
                {
                   
                    TokenEndpointUrl = new Uri($"{pontoEndpoint}/oauth2/token"),
                    ClientId = clientId,
                    ClientSecret = clientSecret,
                    GrantType = GrantType.ClientCredentials
                }
            };
            
            var httpClient = new HttpClient(new OAuthHttpHandler(options));
            httpClient.BaseAddress = new Uri(pontoEndpoint);
            var pontoClient = new MyPontoService(httpClient, 100);
            return pontoClient;
        }

        public  Task<AccountsResponse> GetAccounts()
        {
            return bind(_client.GetAs<AccountsResponse>("accounts"));
        }
        public Task<AccountsResponse> GetAccounts(Uri uri)
        {
            uri = AddLimit(uri);
            return bind(_client.GetAs<AccountsResponse>(uri));
        }
        public Task<TransactionsResponse> GetTransactions(Guid accountId)
        {
            return bind(_client.GetAs<TransactionsResponse>($"accounts/{accountId}/transactions?limit={_pageSize}"));
        }
        public Task<TransactionsResponse> GetTransactions(Uri uri)
        {
            uri = AddLimit(uri);
            return bind(_client.GetAs<TransactionsResponse>(uri));
        }
        public Task<TransactionsResponse> GetTransactionsBefore(Guid accountId, Guid transactionId)
        {
            return bind(_client.GetAs<TransactionsResponse>($"accounts/{accountId}/transactions?before={transactionId}&limit={_pageSize}"));
        }
        public Task<TransactionsResponse> GetTransactionsAfter(Guid accountId, Guid transactionId)
        {
            return bind(_client.GetAs<TransactionsResponse>($"accounts/{accountId}/transactions?after={transactionId}&limit={_pageSize}"));
        }
        public async Task<Synchronization> WaitForSynchronization(Guid synchronizationid, int timeOutInMsSeconds = 10000)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            CancellationToken ct = cancellationTokenSource.Token;
            
            
            cancellationTokenSource.CancelAfter(timeOutInMsSeconds);
            Debug.WriteLine($"CancellationtokenSource started with timeout of {timeOutInMsSeconds}ms");
            await Task.Run(async () =>
            {
                var sync = await GetSynchronization(synchronizationid);
                while (sync.Attributes.Status == "pending" && cancellationTokenSource.IsCancellationRequested == false)
                {
                    System.Diagnostics.Debug.WriteLine($"Sync pending, waiting 1 second");
                    await Task.Delay(TimeSpan.FromSeconds(1));
                    sync = await GetSynchronization(synchronizationid);
                }
            }, ct);
            Debug.WriteLine($"Synchronization task completed");
            cancellationTokenSource.Dispose();
            var sync2 = await GetSynchronization(synchronizationid);
            if (sync2.Attributes.Status != "pending")
            {
                return sync2;
            }
            throw new TimeoutException($"Timeout ({timeOutInMsSeconds.ToString()}ms) elapsed for synchronization- {JsonConvert.SerializeObject(sync2)}");
            
        }

        public async Task<Synchronization> GetSynchronization(Guid synchronizationId)
        {
            var sync = (await _client.GetAs<BasicResponse<Synchronization>>($"synchronizations/{synchronizationId}"))
                .Data;
            sync.Bind(this);
            return sync;
        }

        public async Task<AccountResource> GetAccount(Guid accountId)
        {
            var sync = (await _client.GetAs<BasicResponse<AccountResource>>($"accounts/{accountId}"))
                .Data;
            sync.Bind(this);
            return sync;
        }



        public async Task<Synchronization> CreateSynchronization(Guid accountId, SynchronizationType type= null)
        {
            var response = await _client.PostAsync("synchronizations", new StringContent(JsonConvert.SerializeObject(new
            {
                data = new
                {
                    type = "synchronization",
                    attributes = new
                    {
                        resourceType = "account",
                        resourceId = accountId,
                        subtype = type?.Value ?? SynchronizationType.AccountDetails.Value
                    }
                }
            }), Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                var sync = (await response.GetAs<BasicResponse<Synchronization>>()).Data;
                sync.Bind(this);
                return sync;
            }
            else
            {
                var errorResponse = await response.GetAs<ErrorResponse>();
                throw new MyPontoException(errorResponse);
            }
        }
        public async Task<IReadOnlyCollection<Synchronization>> CreateSynchronizations(Guid accountId)
        {
            var list = new List<Synchronization>();
            list.Add(await CreateSynchronization(accountId, SynchronizationType.AccountDetails));
            list.Add(await CreateSynchronization(accountId, SynchronizationType.AccountTransactions));
            return list;
        }

        private Uri AddLimit(Uri uri)
        {
            return uri.ExtendQuery(new Dictionary<string, string> { { "limit", _pageSize.ToString() } });
        }

        private async Task<AccountsResponse> bind(Task<AccountsResponse> response)
        {
            var accounts = await response;
            accounts.Bind(this);
            return accounts;
        }
        private async Task<Synchronization> bind(Task<Synchronization> response)
        {
            var Synchronization = await response;
            Synchronization.Bind(this);
            return Synchronization;
        }
        
        private async Task<TransactionsResponse> bind(Task<TransactionsResponse> response)
        {
            var transactionsResponse = await response;
            transactionsResponse.Bind(this);
            return transactionsResponse;
        }

    }
}
