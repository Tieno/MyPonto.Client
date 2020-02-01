using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MyPonto.Client.Model
{
    

    public partial class TransactionAttributes
    {
        [JsonProperty("valueDate")]
        
        public DateTimeOffset ValueDate { get; set; }

        [JsonProperty("remittanceInformationType")]
        public string RemittanceInformationType { get; set; }

        [JsonProperty("remittanceInformation")]
        public string RemittanceInformation { get; set; }

        [JsonProperty("executionDate")]
       
        public DateTimeOffset ExecutionDate { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("counterpartReference")]
        public string CounterpartReference { get; set; }

        [JsonProperty("counterpartName")]
        public string CounterpartName { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }
    }
}