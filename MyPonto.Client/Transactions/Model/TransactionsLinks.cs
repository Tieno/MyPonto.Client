using System;
using Newtonsoft.Json;
using Tieno.MyPonto.Client.Model;

namespace Tieno.MyPonto.Client.Transactions.Model
{
    public partial class TransactionsLinks
    {
        [JsonProperty("related")]
        public Uri Related { get; set; }

        [JsonProperty("meta")]
        public LinksMeta Meta { get; set; }
    }
}