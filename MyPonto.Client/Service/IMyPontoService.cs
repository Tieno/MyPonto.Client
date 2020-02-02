using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tieno.MyPonto.Client.Model;

namespace Tieno.MyPonto.Client.Service
{
    public interface IMyPontoService
    {
        Task<AccountsResponse> GetAccounts();
        Task<AccountResource> GetAccount(Guid accountId);

        Task<TransactionsResponse> GetTransactions(Guid accountId);
        Task<TransactionsResponse> GetTransactionsBefore(Guid accountId, Guid transactionId);
        Task<TransactionsResponse> GetTransactionsAfter(Guid accountId, Guid transactionId);

        Task<Synchronization> WaitForSynchronization(Guid synchronizationId, int timeOutInMsSeconds = 10000);
        Task<Synchronization> GetSynchronization(Guid synchronizationId);
   
        Task<Synchronization> CreateSynchronization(Guid accountId, SynchronizationType type= null);
        /// <summary>
        /// attempts to create synchronization resources
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="type"></param>
        /// <returns>2 synchronizations for the account, one for account details and one for transactions, in a "pending" state.
        /// Afterwards you should wait on the synchronizations, by using WaitTillCompleted()
        /// If already synced in the last 30 minutes, returns and empty collection
        /// </returns>
        Task<IReadOnlyCollection<Synchronization>> CreateSynchronizations(Guid accountId);
    }
}