using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MyPonto.Client.Model
{
    public partial class Data
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("id")]
        public Guid Id { get; set; }
    }


    public partial class ErrorResponse
    {
        [JsonProperty("errors")]
        public List<Error> Errors { get; set; }
    }

    public partial class Error
    {
        [JsonProperty("meta")]
        public Meta Meta { get; set; }

        [JsonProperty("detail")]
        public string Detail { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }
    }

}