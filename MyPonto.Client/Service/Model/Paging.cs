using System;
using Newtonsoft.Json;

namespace Tieno.MyPonto.Client.Model
{
    public partial class Paging
    {
        [JsonProperty("limit")]
        public long Limit { get; set; }

        [JsonProperty("before")]
        public Guid? Before { get; set; }

        [JsonProperty("after")]
        public Guid? After { get; set; }
    }
}