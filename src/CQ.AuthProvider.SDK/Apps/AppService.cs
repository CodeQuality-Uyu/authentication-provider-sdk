using CQ.AuthProvider.SDK.Accounts;
using CQ.AuthProvider.SDK.Http;
using Microsoft.Extensions.Options;
namespace CQ.AuthProvider.SDK.Apps;

internal sealed class AppService(
    AuthProviderClient authProviderWebApi,
    IOptions<AuthProviderSection> authProviderOptions)
    : IAppService
{
    private readonly string _subscriptionKey = authProviderOptions.Value.SubscriptionKey ?? string.Empty;

    public async Task<AppCreated> CreateClientAsync(CreateAppChildArgs args, AccountLogged accountLogged)
    {
        var response = await authProviderWebApi
           .PostAsync<AppCreated>("apps/client", args, [new("Authorization", accountLogged.Token)])
           .ConfigureAwait(false);

        return response;
    }

    public async Task<AppCreated> CreateClientWithSubscriptionAsync(CreateAppChildArgs args)
    {
        var response = await authProviderWebApi
           .PostAsync<AppCreated>("apps/client", args, [new("Authorization", $"Subscription {_subscriptionKey}")])
           .ConfigureAwait(false);

        return response;
    }

    public async Task<AppDetailedInfo> GetByIdAsync(Guid id, AccountLogged accountLogged)
    {
        var response = await authProviderWebApi
           .GetAsync<AppDetailedInfo>($"apps/{id}", [new("Authorization", accountLogged.Token)])
           .ConfigureAwait(false);

        return response;
    }

    public async Task<AppDetailedInfo> GetByIdWithSubscriptionAsync(Guid id)
    {
        var response = await authProviderWebApi
           .GetAsync<AppDetailedInfo>($"apps/{id}", [new("Subscription", _subscriptionKey)])
           .ConfigureAwait(false);

        return response;
    }
}
