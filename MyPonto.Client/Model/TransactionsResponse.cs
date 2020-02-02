using System.Collections.Generic;
using Newtonsoft.Json;
using Tieno.MyPonto.Client.Service;

namespace Tieno.MyPonto.Client.Model
{
    public partial class TransactionsResponse
    {
        [JsonProperty("meta")]
        public Meta Meta { get; set; }

        [JsonProperty("links")]
        public TransactionsResponseLinks Links { get; set; }


        [JsonProperty("data")] 
        public List<TransactionResource> Data { get; set; }

        internal void Bind(MyPontoService service)
        {
            Links.Bind(service);
        }
    }
}
