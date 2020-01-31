using Newtonsoft.Json;

namespace MyPonto.Client.Model
{
    public partial class AccountResourceAttributes
    {
        [JsonProperty("subtype")]
        public string Subtype { get; set; }

        [JsonProperty("referenceType")]
        public string ReferenceType { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("currentBalance")]
        public decimal CurrentBalance { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("availableBalance")]
        public decimal AvailableBalance { get; set; }
    }
}