using System;
using System.Threading.Tasks;
using Tieno.MyPonto.Client.Accounts.Model;

namespace Tieno.MyPonto.Client.Accounts
{
    public interface IAccountApi
    {
        Task<AccountsResponse> GetAccounts();
        Task<AccountResource> GetAccount(Guid accountId);
    }
}