using CQ.AuthProvider.SDK.Accounts;

namespace CQ.AuthProvider.SDK.Me;

public interface IMeService
{
    Task<AccountLogged> GetAsync(string token);
}