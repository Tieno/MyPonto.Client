using Newtonsoft.Json;

namespace MyPonto.Client.Model
{
    public partial class AccountResponseMeta
    {
        [JsonProperty("paging")]
        public Paging Paging { get; set; }
    }
}