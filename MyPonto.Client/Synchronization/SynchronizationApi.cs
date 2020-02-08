using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tieno.MyPonto.Client.Model;
using Tieno.MyPonto.Client.Service;

namespace Tieno.MyPonto.Client.Synchronization
{
    internal class SynchronizationApi: BaseResourceApi, ISynchronizationApi
    {
        internal SynchronizationApi(HttpClient client, IMyPontoApi myPontoApi) : base(client, myPontoApi)
        {
        }

        internal SynchronizationApi(HttpClient client, int pageSize, IMyPontoApi myPontoApi) : base(client, pageSize, myPontoApi)
        {
        }
        public async Task<Model.Synchronization> WaitForSynchronization(Guid synchronizationid, int timeOutInMsSeconds = 10000)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            CancellationToken ct = cancellationTokenSource.Token;


            cancellationTokenSource.CancelAfter(timeOutInMsSeconds);
            Debug.WriteLine($"CancellationtokenSource started with timeout of {timeOutInMsSeconds}ms");
            await Task.Run(async () =>
            {
                var sync = await GetSynchronization(synchronizationid);
                while (sync.Attributes.Status == "pending" && cancellationTokenSource.IsCancellationRequested == false)
                {
                    System.Diagnostics.Debug.WriteLine($"Sync pending, waiting 1 second");
                    await Task.Delay(TimeSpan.FromSeconds(1));
                    sync = await GetSynchronization(synchronizationid);
                }
            }, ct);
            Debug.WriteLine($"Synchronization task completed");
            cancellationTokenSource.Dispose();
            var sync2 = await GetSynchronization(synchronizationid);
            if (sync2.Attributes.Status != "pending")
            {
                return sync2;
            }
            throw new TimeoutException($"Timeout ({timeOutInMsSeconds.ToString()}ms) elapsed for synchronization- {JsonConvert.SerializeObject(sync2)}");

        }

        public async Task<Model.Synchronization> GetSynchronization(Guid synchronizationId)
        {
            var sync = (await _client.GetAs<BasicResponse<Model.Synchronization>>($"synchronizations/{synchronizationId}"))
                .Data;
            sync.Bind(this._myPontoApi);
            return sync;
        }


        public async Task<Model.Synchronization> CreateSynchronization(Guid accountId, SynchronizationType type = null)
        {
            var response = await _client.PostAsync("synchronizations", new StringContent(JsonConvert.SerializeObject(new
            {
                data = new
                {
                    type = "synchronization",
                    attributes = new
                    {
                        resourceType = "account",
                        resourceId = accountId,
                        subtype = type?.Value ?? SynchronizationType.AccountDetails.Value
                    }
                }
            }), Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                var sync = (await response.GetAs<BasicResponse<Model.Synchronization>>()).Data;
                sync.Bind(this._myPontoApi);
                return sync;
            }
            else
            {
                var errorResponse = await response.GetAs<ErrorResponse>();
                throw new MyPontoException(errorResponse);
            }
        }
        public async Task<IReadOnlyCollection<Model.Synchronization>> CreateSynchronizations(Guid accountId)
        {
            var list = new List<Model.Synchronization>();
            list.Add(await CreateSynchronization(accountId, SynchronizationType.AccountDetails));
            list.Add(await CreateSynchronization(accountId, SynchronizationType.AccountTransactions));
            return list;
        }
    }
}