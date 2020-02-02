using Newtonsoft.Json;

namespace Tieno.MyPonto.Client.Model
{
    public partial class AccountRelationship
    {
        [JsonProperty("links")] public AccountLinks Links { get; set; }

        [JsonProperty("data")] public Data Data { get; set; }

    }
}