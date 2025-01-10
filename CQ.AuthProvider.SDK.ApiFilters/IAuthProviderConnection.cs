using CQ.AuthProvider.SDK.ApiFilters.Accounts;

namespace CQ.AuthProvider.SDK.ApiFilters;

internal interface IAuthProviderConnection
{
    #region Session
    Task<AccountLogged> GetMeAsync(string token);
    #endregion
}
