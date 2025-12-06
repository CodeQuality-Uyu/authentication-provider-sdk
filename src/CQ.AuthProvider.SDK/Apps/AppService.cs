using CQ.AuthProvider.SDK.Accounts;
using CQ.AuthProvider.SDK.Http;
namespace CQ.AuthProvider.SDK.Apps;

internal sealed class AppService(
    AuthProviderConnectionApi authProviderWebApi)
    : IAppService
{
    public async Task<AppCreated> CreateAsync(CreateAppChildArgs args, AccountLogged accountLogged)
    {
        var response = await authProviderWebApi
           .PostAsync<AppCreated>("apps/client", args, [new("Authorization", accountLogged.Token)])
           .ConfigureAwait(false);

        return response;
    }

    public async Task<AppDetailedInfo> GetAsync(Guid id, AccountLogged accountLogged)
    {
        var response = await authProviderWebApi
           .GetAsync<AppDetailedInfo>($"apps/{id}", [new("Authorization", accountLogged.Token)])
           .ConfigureAwait(false);

        return response;
    }
}
