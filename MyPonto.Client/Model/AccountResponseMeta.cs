using Newtonsoft.Json;

namespace Tieno.MyPonto.Client.Model
{
    public partial class AccountResponseMeta
    {
        [JsonProperty("paging")]
        public Paging Paging { get; set; }
    }
}