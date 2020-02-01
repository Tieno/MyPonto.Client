using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using MyPonto.Client.Model;

namespace MyPonto.Client.Service
{
    public interface IMyPontoService
    {
        Task<AccountsResponse> GetAccounts();
        Task<AccountResource> GetAccount(Guid accountId);

        Task<TransactionsResponse> GetTransactions(Guid accountId);
        Task<TransactionsResponse> GetTransactionsBefore(Guid accountId, Guid transactionId);
        Task<TransactionsResponse> GetTransactionsAfter(Guid accountId, Guid transactionId);

        Task<Synchronization> WaitForSynchronization(Guid synchronizationid, int timeOutInMsSeconds = 10000);
        Task<Synchronization> GetSynchronization(Guid synchronizationId);
        Task<Synchronization> CreateSynchronization(Guid accountId, SynchronizationType type= null);
        Task<IReadOnlyCollection<Synchronization>> CreateSynchronizations(Guid accountId);
    }
}