using System;
using Newtonsoft.Json;
using Tieno.MyPonto.Client.Model;
using Tieno.MyPonto.Client.Service;

namespace Tieno.MyPonto.Client.Accounts.Model
{
    public partial class AccountResource
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("relationships")]
        public Relationships Relationships { get; set; }

        [JsonProperty("meta")]
        public AccountResourceMeta Meta { get; set; }

        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("attributes")]
        public AccountResourceAttributes Attributes { get; set; }

        internal void Bind(IMyPontoApi api)
        {
            this.Relationships.Bind(api);
        }
    }
}