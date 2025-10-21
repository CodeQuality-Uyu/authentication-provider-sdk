using CQ.AuthProvider.SDK.Accounts;
using CQ.AuthProvider.SDK.Http;

namespace CQ.AuthProvider.SDK.Me;

internal sealed class MeService(AuthProviderConnectionApi authProviderWebApi)
: IMeService
{
    public async Task<AccountLogged> GetAsync(string token)
    {
        var response = await authProviderWebApi.GetAsync<AccountLogged>(
           $"me",
           [new("Authorization", token)])
           .ConfigureAwait(false);

        return response;
    }
}