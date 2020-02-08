using Newtonsoft.Json;
using Tieno.MyPonto.Client.Model;

namespace Tieno.MyPonto.Client.Accounts.Model
{
    public partial class AccountRelationship
    {
        [JsonProperty("links")] public AccountLinks Links { get; set; }

        [JsonProperty("data")] public Data Data { get; set; }

    }
}