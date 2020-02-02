using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MyPonto.Client.Model
{
    public partial class LatestSynchronizationAttributes
    {
        [JsonProperty("updatedAt")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("subtype")]
        public string Subtype { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("resourceType")]
        public string ResourceType { get; set; }

        [JsonProperty("resourceId")]
        public Guid ResourceId { get; set; }

        [JsonProperty("errors")]
        public List<object> Errors { get; set; }

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }
    }
}