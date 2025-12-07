using CQ.AuthProvider.SDK.Accounts;
using CQ.AuthProvider.SDK.Http;

namespace CQ.AuthProvider.SDK.Me;

internal sealed class MeService(AuthProviderClient authProviderWebApi)
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

    public async Task UpdateAsync(
        UpdateMeArgs args,
        AccountLogged accountLogged)
    {
        await authProviderWebApi
        .UpdateVoidAsync<CqAuthErrorApi>($"me", args, [new ("Authorization", accountLogged.Token)])
        .ConfigureAwait(false);
    }
}