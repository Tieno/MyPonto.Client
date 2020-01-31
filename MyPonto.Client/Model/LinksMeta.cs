using Newtonsoft.Json;

namespace MyPonto.Client.Model
{
    public partial class LinksMeta
    {
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}