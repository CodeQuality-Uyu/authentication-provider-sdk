using CQ.AuthProvider.SDK.ApiFilters.Accounts;
using CQ.Utility;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

namespace CQ.AuthProvider.SDK.ApiFilters;

internal sealed class AuthProviderConnectionApi(IOptions<AuthProviderSection> _section)
    : ConcreteHttpClient<CqAuthErrorApi>(_section.Value.Server),
    IAuthProviderConnection
{
    protected override Exception? ProcessConcreteError(CqAuthErrorApi error) => new CqAuthException(error);

    #region Me
    public async Task<AccountLogged> GetMeAsync(string token)
    {
        var response = await GetAsync<AccountLogged>(
            $"me",
            [new(HeaderNames.Authorization, token)])
            .ConfigureAwait(false);

        return response;
    }
    #endregion
}
