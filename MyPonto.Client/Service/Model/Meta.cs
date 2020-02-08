using System;
using Newtonsoft.Json;

namespace Tieno.MyPonto.Client.Model
{
    public partial class Meta
    {
        [JsonProperty("synchronizedAt")]
        public DateTime? SynchronizedAt { get; set; }

        [JsonProperty("paging")]
        public Paging Paging { get; set; }

        [JsonProperty("latestSynchronization")]
        public Synchronization.Model.Synchronization LatestSynchronization { get; set; }
    }
}