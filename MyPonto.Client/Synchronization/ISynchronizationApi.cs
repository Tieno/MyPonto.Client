using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tieno.MyPonto.Client.Service;

namespace Tieno.MyPonto.Client.Synchronization
{
    public interface ISynchronizationApi
    {
        Task<Model.Synchronization> WaitForSynchronization(Guid synchronizationid, int timeOutInMsSeconds = 10000);
        Task<Model.Synchronization> GetSynchronization(Guid synchronizationId);
        Task<Model.Synchronization> CreateSynchronization(Guid accountId, SynchronizationType type = null);
        Task<IReadOnlyCollection<Model.Synchronization>> CreateSynchronizations(Guid accountId);
    }
}