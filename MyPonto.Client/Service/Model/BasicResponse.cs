using Newtonsoft.Json;

namespace Tieno.MyPonto.Client.Model
{
    public class BasicResponse<T>
    {
        [JsonProperty("data")]
        public T Data { get; set; }
    }
}