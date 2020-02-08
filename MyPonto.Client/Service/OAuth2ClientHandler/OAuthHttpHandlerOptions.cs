using System.Net.Http;
using Tieno.MyPonto.Client.OAuth2ClientHandler.Authorizer;

namespace Tieno.MyPonto.Client.OAuth2ClientHandler
{
    public sealed class OAuthHttpHandlerOptions
    {
        public AuthorizerOptions AuthorizerOptions { get; set; }
        public HttpMessageHandler InnerHandler { get; set; }

        public OAuthHttpHandlerOptions()
        {
            AuthorizerOptions = new AuthorizerOptions();
        }
    }
}
