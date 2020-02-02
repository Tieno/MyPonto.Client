using System;
using Newtonsoft.Json;

namespace Tieno.MyPonto.Client.Model
{
    public partial class TransactionsLinks
    {
        [JsonProperty("related")]
        public Uri Related { get; set; }

        [JsonProperty("meta")]
        public LinksMeta Meta { get; set; }
    }
}