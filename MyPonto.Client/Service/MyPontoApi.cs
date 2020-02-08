using System;
using System.Net.Http;
using Tieno.MyPonto.Client.Accounts;
using Tieno.MyPonto.Client.Model;
using Tieno.MyPonto.Client.OAuth2ClientHandler;
using Tieno.MyPonto.Client.OAuth2ClientHandler.Authorizer;
using Tieno.MyPonto.Client.Synchronization;
using Tieno.MyPonto.Client.Transactions;

namespace Tieno.MyPonto.Client.Service
{
    public class MyPontoApi : IMyPontoApi
    {
        protected MyPontoApi()
        {
            
        }
        public static IMyPontoApi Create(string clientId, string clientSecret, int pageSize = 100, string pontoEndpoint = "https://api.myponto.com")
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
            var pontoClient = new MyPontoApi();
            pontoClient.Accounts = new AccountApi(httpClient, pageSize, pontoClient);
            pontoClient.Synchronizations = new SynchronizationApi(httpClient, pageSize, pontoClient);
            pontoClient.Transactions = new TransactionApi(httpClient, pageSize, pontoClient);
            return pontoClient;
        }

        public IAccountApi Accounts { get; set; }
        public ITransactionApi Transactions { get; set; }
        public ISynchronizationApi Synchronizations { get; set; }
    }
}
