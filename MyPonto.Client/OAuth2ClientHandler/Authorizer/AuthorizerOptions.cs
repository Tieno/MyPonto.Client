﻿using System;
using System.Collections.Generic;
using System.Net;

namespace Tieno.MyPonto.Client.OAuth2ClientHandler.Authorizer
{
    public sealed class AuthorizerOptions
    {
        public Uri TokenEndpointUrl { get; set; }
        public Uri AuthorizeEndpointUrl { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public IEnumerable<string> Scope { get; set; }
        public GrantType GrantType { get; set; }
        public CredentialTransportMethod CredentialTransportMethod { get; set; }
        public Action<HttpStatusCode, string> OnError { get; set; }

        public AuthorizerOptions()
        {
            CredentialTransportMethod = CredentialTransportMethod.BasicAuthenticationCredentials;
        }
    }
}
