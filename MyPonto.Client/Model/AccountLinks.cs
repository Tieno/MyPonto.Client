﻿using System;
using Newtonsoft.Json;

namespace Tieno.MyPonto.Client.Model
{
    public partial class AccountLinks
    {
        [JsonProperty("related")]
        public Uri Related { get; set; }
    }
}