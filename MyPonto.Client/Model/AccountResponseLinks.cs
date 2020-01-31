using System;
using Newtonsoft.Json;

namespace MyPonto.Client.Model
{
    public partial class AccountResponseLinks
    {
        [JsonProperty("first")]
        public Uri First { get; set; }
    }
}