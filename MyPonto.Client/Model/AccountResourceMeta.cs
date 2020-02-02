using System;
using Newtonsoft.Json;

namespace MyPonto.Client.Model
{
    public partial class AccountResourceMeta
    {
        [JsonProperty("synchronizedAt")]
        public DateTime SynchronizedAt { get; set; }

        [JsonProperty("latestSynchronization")]
        public Synchronization LatestSynchronization { get; set; }
    }

}