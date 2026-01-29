
using CQ.AuthProvider.SDK.Http;
using CQ.AuthProvider.SDK.Sessions;
using Microsoft.Extensions.Options;

namespace CQ.AuthProvider.SDK.Accounts;

internal sealed class AccountService(
    AuthProviderClient authProviderWebApi,
    IOptions<AuthProviderSection> authProviderOptions)
: IAccountService
{
    private readonly string _subscriptionKey = authProviderOptions.Value.SubscriptionKey ?? string.Empty;

    public async Task<SessionCreated> CreateAsync(CreateAccountPasswordArgs args)
    {
        var response = await authProviderWebApi
            .PostAsync<SessionCreated>("accounts/credentials", args, [])
            .ConfigureAwait(false);

        return response;
    }

    public async Task<AccountCreated> CreateForAsync(CreateAccountForArgs args, AccountLogged accountLogged)
    {
        var response = await authProviderWebApi
            .PostAsync<AccountCreated>("accounts/credentials/for", args, [new("Authorization", accountLogged.Token)])
            .ConfigureAwait(false);

        return response;
    }

    public async Task<AccountCreated> CreateForWithSubscriptionAsync(CreateAccountForArgs args)
    {
        var response = await authProviderWebApi
            .PostAsync<AccountCreated>("accounts/credentials/for", args, [new("Authorization", _subscriptionKey)])
            .ConfigureAwait(false);

        return response;
    }
}