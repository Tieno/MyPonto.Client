using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tieno.MyPonto.Client.Service;

namespace Tieno.MyPonto.Client.Synchronization.Model
{
    public partial class Synchronization: FetchableResource
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("attributes")]
        public LatestSynchronizationAttributes Attributes { get; set; }

        public Task WaitTillCompleted(int timeOutInMsSeconds = 10000)
        {
            if (this.Attributes.Status == "pending")
            {
                return myPontoClient.Synchronizations.WaitForSynchronization(this.Id, timeOutInMsSeconds);
            }
            else
            {
                return Task.CompletedTask;
            }
            
        }
    }
}