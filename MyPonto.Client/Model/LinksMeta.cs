using Newtonsoft.Json;

namespace Tieno.MyPonto.Client.Model
{
    public partial class LinksMeta
    {
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}