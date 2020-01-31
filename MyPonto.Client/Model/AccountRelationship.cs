using System.Threading.Tasks;
using MyPonto.Client.Service;
using Newtonsoft.Json;

namespace MyPonto.Client.Model
{
    public partial class AccountRelationship
    {
        [JsonProperty("links")] public AccountLinks Links { get; set; }

        [JsonProperty("data")] public Data Data { get; set; }

    }
}