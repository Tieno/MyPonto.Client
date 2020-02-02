using System;
using Newtonsoft.Json;

namespace Tieno.MyPonto.Client.Model
{
    public partial class FinancialInstitutionLinks
    {
        [JsonProperty("related")]
        public Uri Related { get; set; }
    }
}