using System.Threading;
using System.Threading.Tasks;

namespace Tieno.MyPonto.Client.OAuth2ClientHandler.Authorizer
{
    internal interface IAuthorizer
    {
        Task<TokenResponse> GetToken(CancellationToken? cancellationToken = null);
    }
}
