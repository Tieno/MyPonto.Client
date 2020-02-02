using Newtonsoft.Json;

namespace Tieno.MyPonto.Client.Model
{
    public partial class FinancialInstitutionRelationships
    {
        [JsonProperty("links")]
        public FinancialInstitutionLinks Links { get; set; }

        [JsonProperty("data")]
        public Data Data { get; set; }
    }
}