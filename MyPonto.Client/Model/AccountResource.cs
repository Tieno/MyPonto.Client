﻿using System;
using MyPonto.Client.Service;
using Newtonsoft.Json;

namespace MyPonto.Client.Model
{
    public partial class AccountResource
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("relationships")]
        public Relationships Relationships { get; set; }

        [JsonProperty("meta")]
        public AccountResourceMeta Meta { get; set; }

        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("attributes")]
        public AccountResourceAttributes Attributes { get; set; }

        internal void Bind(MyPontoService service)
        {
            this.Relationships.Bind(service);
        }
    }
}