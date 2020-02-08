using System;
using Newtonsoft.Json;

namespace Tieno.MyPonto.Client.Accounts.Model
{
    public partial class AccountResourceMeta
    {
        [JsonProperty("synchronizedAt")]
        public DateTime SynchronizedAt { get; set; }

        [JsonProperty("latestSynchronization")]
        public Synchronization.Model.Synchronization LatestSynchronization { get; set; }
    }

}