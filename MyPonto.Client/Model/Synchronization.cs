using System;
using System.Threading.Tasks;
using MyPonto.Client.Service;
using Newtonsoft.Json;

namespace MyPonto.Client.Model
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
                return _service.WaitForSynchronization(this.Id, timeOutInMsSeconds);
            }
            else
            {
                return Task.CompletedTask;
            }
            
        }
    }
}