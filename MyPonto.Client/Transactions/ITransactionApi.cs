using System;
using System.Threading.Tasks;
using Tieno.MyPonto.Client.Transactions.Model;

namespace Tieno.MyPonto.Client.Transactions
{
    public interface ITransactionApi
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="transactionId"></param>
        /// <returns></returns>
        Task<TransactionResource> GetTransaction(Guid accountId, Guid transactionId);
        /// <summary>
        /// List Transactions
        /// </summary>
        /// <param name="accountId">Identifier for the corresponding account</param>
        /// <returns>a list of transaction resources.</returns>
        Task<TransactionsResponse> GetTransactions(Guid accountId);
        Task<TransactionsResponse> GetTransactions(Uri uri);
        /// <summary>
        /// List Transactions
        /// </summary>
        /// <param name="accountId">Identifier for the corresponding account</param>
        /// <param name="transactionId">Cursor for pagination. Indicates that the API should return the transaction resources which are immediately before this one in the list (the previous page)</param>
        /// <returns>a list of transaction resources.</returns>
        Task<TransactionsResponse> GetTransactionsBefore(Guid accountId, Guid transactionId);
        /// <summary>
        /// List Transactions
        /// </summary>
        /// <param name="accountId">Identifier for the corresponding account</param>
        /// <param name="transactionId">Cursor for pagination. Indicates that the API should return the transaction resources which are immediately after this one in the list (the next page)</param>
        /// <returns>a list of transaction resources.</returns>
        Task<TransactionsResponse> GetTransactionsAfter(Guid accountId, Guid transactionId);
    }
}