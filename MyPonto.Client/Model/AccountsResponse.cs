using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyPonto.Client.Service;

namespace MyPonto.Client.Model
{
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

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
