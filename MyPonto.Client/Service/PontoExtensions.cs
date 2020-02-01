using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MyPonto.Client.Model;

namespace MyPonto.Client.Service
{
    public static class PontoExtensions
    {
        public static async Task<TransactionsResponse> GetAllTransactions(this IMyPontoService myPontoService, Guid accountId)
        {
            var allTransactionsResponse = new TransactionsResponse();
            allTransactionsResponse.Data = new List<TransactionResource>();
            
            var transactionResponse = await myPontoService.GetTransactions(accountId);
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
        public static async Task<TransactionsResponse> GetNewTransactions(this IMyPontoService myPontoService, Guid accountId, Guid lastKnownTransactionId)
        {
            var allTransactionsResponse = new TransactionsResponse();
            allTransactionsResponse.Data = new List<TransactionResource>();
            var transactionResponse = await myPontoService.GetTransactionsBefore(accountId, lastKnownTransactionId);
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
            if (synchronizedAt.AddMinutes(30) > DateTimeOffset.Now)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static async Task<IReadOnlyCollection<Synchronization>> SynchronizeAccount(this IMyPontoService myPontoService, Guid accountId)
        {
            var account = await myPontoService.GetAccount(accountId);
            if (account.Meta.SynchronizedAt.CanSynchronize())
            {
                return await myPontoService.CreateSynchronizations(accountId);
            }
            return new List<Synchronization>();
        }
        

         

        public static async Task WaitTillCompleted(this IEnumerable<Synchronization> syncs, int timeOutInMsSeconds = 10000)
        {
            foreach (var synchronization in syncs)
            {
                await synchronization.WaitTillCompleted(timeOutInMsSeconds);
            }
        }
    }
}