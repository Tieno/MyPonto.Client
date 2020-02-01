using System;
using System.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MyPonto.Client.Model
{
   
    public partial class TransactionResource
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("relationships")]
        public Relationships Relationships { get; set; }

        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("attributes")]
        public TransactionAttributes Attributes { get; set; }

        
    }
}