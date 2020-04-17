using System;
using System.Net.Http;
using System.Threading.Tasks;
using Tieno.MyPonto.Client.Accounts.Model;
using Tieno.MyPonto.Client.Model;
using Tieno.MyPonto.Client.Service;

namespace Tieno.MyPonto.Client.Accounts
{
    internal class AccountApi: BaseResourceApi, IAccountApi
    {
        internal AccountApi(HttpClient client, IMyPontoApi myPontoApi) : base(client, myPontoApi)
        {
        }

        internal AccountApi(HttpClient client, int pageSize, IMyPontoApi myPontoApi) : base(client, pageSize, myPontoApi)
        {
        }

        public Task<AccountsResponse> GetAccounts()
        {
            return bind(_client.GetAs<AccountsResponse>("accounts"));
        }
        public Task<AccountsResponse> GetAccounts(Uri uri)
        {
            uri = AddLimit(uri);
            return bind(_client.GetAs<AccountsResponse>(uri));
        }
        public async Task<AccountResource> GetAccount(Guid accountId)
        {
            var account = (await _client.GetAs<BasicResponse<AccountResource>>($"accounts/{accountId}"))
                .Data;
            account.Bind(this._myPontoApi);
            return account;
        }
    }



}
