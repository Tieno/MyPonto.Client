using Newtonsoft.Json;

namespace MyPonto.Client.Model
{
    public partial class FinancialInstitutionRelationships
    {
        [JsonProperty("links")]
        public FinancialInstitutionLinks Links { get; set; }

        [JsonProperty("data")]
        public Data Data { get; set; }
    }
}