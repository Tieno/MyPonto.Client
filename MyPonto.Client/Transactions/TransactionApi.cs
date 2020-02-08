using System;
using System.Net.Http;
using System.Threading.Tasks;
using Tieno.MyPonto.Client.Service;
using Tieno.MyPonto.Client.Transactions.Model;

namespace Tieno.MyPonto.Client.Transactions
{
    internal class TransactionApi: BaseResourceApi, ITransactionApi
    {
        internal TransactionApi(HttpClient client, IMyPontoApi myPontoApi) : base(client, myPontoApi)
        {
        }

        internal TransactionApi(HttpClient client, int pageSize, IMyPontoApi myPontoApi) : base(client, pageSize, myPontoApi)
        {
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
    }
}