﻿using System;
using Newtonsoft.Json;

namespace MyPonto.Client.Model
{
    public partial class AccountLinks
    {
        [JsonProperty("related")]
        public Uri Related { get; set; }
    }
}