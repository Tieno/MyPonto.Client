using System;
using Newtonsoft.Json;

namespace Tieno.MyPonto.Client.Accounts.Model
{
    public partial class AccountResponseLinks
    {
        [JsonProperty("first")]
        public Uri First { get; set; }
    }
}