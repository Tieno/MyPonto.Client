using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Tieno.MyPonto.Client.Accounts.Model;
using Tieno.MyPonto.Client.Transactions.Model;

namespace Tieno.MyPonto.Client.Service
{
    internal class BaseResourceApi
    {
        protected Uri AddLimit(Uri uri)
        {
            return uri.ExtendQuery(new Dictionary<string, string> { { "limit", _pageSize.ToString() } });
        }

        protected async Task<AccountsResponse> bind(Task<AccountsResponse> response)
        {
            var accounts = await response;
            accounts.Bind(this._myPontoApi);
            return accounts;
        }
        protected async Task<Synchronization.Model.Synchronization> bind(Task<Synchronization.Model.Synchronization> response)
        {
            var Synchronization = await response;
            Synchronization.Bind(this._myPontoApi);
            return Synchronization;
        }

        protected async Task<TransactionsResponse> bind(Task<TransactionsResponse> response)
        {
            var transactionsResponse = await response;
            transactionsResponse.Bind(this._myPontoApi);
            return transactionsResponse;
        }
        
        protected readonly HttpClient _client;
        protected readonly IMyPontoApi _myPontoApi;
        protected readonly int _pageSize;

        internal BaseResourceApi(HttpClient client, IMyPontoApi myPontoApi)
        {
            _client = client;
            _myPontoApi = myPontoApi;
            _pageSize = 20;

        }
        internal BaseResourceApi(HttpClient client, int pageSize, IMyPontoApi myPontoApi)
        {
            _client = client;
            _pageSize = pageSize;
            _myPontoApi = myPontoApi;
        }
    }
}