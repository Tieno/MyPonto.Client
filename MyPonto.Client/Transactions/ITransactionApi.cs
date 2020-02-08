using System;
using System.Threading.Tasks;
using Tieno.MyPonto.Client.Transactions.Model;

namespace Tieno.MyPonto.Client.Transactions
{
    public interface ITransactionApi
    {
        Task<TransactionsResponse> GetTransactions(Guid accountId);
        Task<TransactionsResponse> GetTransactions(Uri uri);
        Task<TransactionsResponse> GetTransactionsBefore(Guid accountId, Guid transactionId);
        Task<TransactionsResponse> GetTransactionsAfter(Guid accountId, Guid transactionId);
    }
}