using System.Collections.Generic;
using Newtonsoft.Json;
using Tieno.MyPonto.Client.Service;

namespace Tieno.MyPonto.Client.Model
{
    public partial class AccountsResponse
    {
        [JsonProperty("meta")]
        public AccountResponseMeta Meta { get; set; }

        [JsonProperty("links")]
        public AccountResponseLinks Links { get; set; }

        [JsonProperty("data")]
        public List<AccountResource> Data { get; set; }

        internal void Bind(MyPontoService service)
        {
            foreach (var accountResource in Data)
            {
                accountResource.Relationships.Bind(service);
            }
        }

       
    }
}
