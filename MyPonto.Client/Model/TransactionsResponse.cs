using System.Collections.Generic;
using MyPonto.Client.Service;
using Newtonsoft.Json;

namespace MyPonto.Client.Model
{
    public partial class TransactionsResponse
    {
        [JsonProperty("meta")]
        public Meta Meta { get; set; }

        [JsonProperty("links")]
        public TransactionsResponseLinks Links { get; set; }

        [JsonProperty("data")]
        public SortedSet<TransactionResource> Data { get; set; }

        internal void Bind(MyPontoService service)
        {
            Links.Bind(service);
        }
    }
}
