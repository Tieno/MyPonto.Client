﻿using System;
using Newtonsoft.Json;

namespace Tieno.MyPonto.Client.Model
{
    public partial class AccountResponseLinks
    {
        [JsonProperty("first")]
        public Uri First { get; set; }
    }
}