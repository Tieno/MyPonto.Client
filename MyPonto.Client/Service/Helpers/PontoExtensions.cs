using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Tieno.MyPonto.Client.Model;
using Tieno.MyPonto.Client.Transactions;
using Tieno.MyPonto.Client.Transactions.Model;

namespace Tieno.MyPonto.Client.Service
{
    
    public static class PontoExtensions
    {
        public static async Task<TransactionsResponse> GetAllTransactions(this ITransactionApi myPontoApi, Guid accountId)
        {
            var allTransactionsResponse = new TransactionsResponse();
            allTransactionsResponse.Data = new List<TransactionResource>();
            
            var transactionResponse = await myPontoApi.GetTransactions(accountId);
            allTransactionsResponse.Meta = transactionResponse.Meta;
            allTransactionsResponse.Meta.Paging = null;
            TransactionsResponse nextPage = await transactionResponse.Links.GetFirstPage();
            allTransactionsResponse.Data.AddRange(nextPage.Data);
            if (nextPage.Links.Next != null)
            {
                do
                {
                    nextPage = await nextPage.Links.GetNextPage();
                    allTransactionsResponse.Data.AddRange(nextPage.Data);
                    allTransactionsResponse.Links = nextPage.Links;
                } while (nextPage.Links.Next != null);
            }
          
            return allTransactionsResponse;
        }
        public static async Task<TransactionsResponse> GetNewTransactions(this ITransactionApi myPontoApi, Guid accountId, Guid lastKnownTransactionId)
        {
            var allTransactionsResponse = new TransactionsResponse();
            allTransactionsResponse.Data = new List<TransactionResource>();
            var transactionResponse = await myPontoApi.GetTransactionsBefore(accountId, lastKnownTransactionId);
            allTransactionsResponse.Meta = transactionResponse.Meta;
            allTransactionsResponse.Meta.Paging = null;
            allTransactionsResponse.Data.AddRange(transactionResponse.Data);
            if (transactionResponse.Links.Prev != null)
            {
                do
                {
                    transactionResponse = await transactionResponse.Links.GetPreviousPage();
                    allTransactionsResponse.Data.AddRange(transactionResponse.Data);
                    allTransactionsResponse.Links = transactionResponse.Links;
                } while (transactionResponse.Links.Prev != null);
            }
        
            return allTransactionsResponse;
        }

        private static bool CanSynchronize(this DateTimeOffset synchronizedAt)
        {
            var syncedAt = synchronizedAt.AddMinutes(30).ToUniversalTime();
            var now = DateTimeOffset.UtcNow;
            Debug.WriteLine($"{nameof(syncedAt)}={syncedAt}, {nameof(now)}={now}. CanSynchronize() == SyncedAt < Now == {syncedAt < now}");
            if (syncedAt < now)
            {

                return true;
            }
            else
            {
                return false;
            }
        }
        private static bool CanSynchronize(this DateTime synchronizedAt)
        {
            var syncedAt = synchronizedAt.AddMinutes(30).ToUniversalTime();
            var now = DateTime.UtcNow;
            Debug.WriteLine($"{nameof(syncedAt)}={syncedAt}, {nameof(now)}={now}. CanSynchronize() == SyncedAt < Now == {syncedAt < now}");
            if (syncedAt < now)
            {

                return true;
            }
            else
            {
                return false;
            }
        }

        public static async Task<IReadOnlyCollection<Synchronization.Model.Synchronization>> SynchronizeAccount(this IMyPontoApi myPontoApi, Guid accountId)
        {
            var account = await myPontoApi.Accounts.GetAccount(accountId);
            if (account.Meta.SynchronizedAt.CanSynchronize())
            {
                return await myPontoApi.Synchronizations.CreateSynchronizations(accountId);
            }
            return new List<Synchronization.Model.Synchronization>();
        }
        

         

        public static Task WaitTillCompleted(this IEnumerable<Synchronization.Model.Synchronization> syncs, int timeOutInMsSeconds = 10000)
        {
            return Task.WhenAll(syncs.Select(x => x.WaitTillCompleted()));
        }
    }
}