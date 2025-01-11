using CQ.AuthProvider.SDK.ApiFilters.Accounts;
using CQ.Utility;
using Microsoft.Net.Http.Headers;

namespace CQ.AuthProvider.SDK.ApiFilters;

internal sealed class AuthProviderConnectionApi(AuthProviderSection _section)
    : ConcreteHttpClient<CqAuthErrorApi>(_section.Server),
    IAuthProviderConnection
{
    protected override Exception? ProcessError(CqAuthErrorApi error)
    {
        return new CqAuthException(error);
    }

    #region Me
    public async Task<AccountLogged> GetMeAsync(string token)
    {
        var response = await GetAsync<AccountLogged>(
            $"me",
            [new(HeaderNames.Authorization, token ?? "a")])
            .ConfigureAwait(false);

        return response;
    }
    #endregion
}
