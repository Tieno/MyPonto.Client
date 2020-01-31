using System;
using Newtonsoft.Json;

namespace MyPonto.Client.Model
{
    public partial class Meta
    {
        [JsonProperty("synchronizedAt")]
        public DateTimeOffset? SynchronizedAt { get; set; }

        [JsonProperty("paging")]
        public Paging Paging { get; set; }

        [JsonProperty("latestSynchronization")]
        public Synchronization LatestSynchronization { get; set; }
    }
}