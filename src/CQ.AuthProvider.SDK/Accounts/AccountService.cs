
using CQ.AuthProvider.SDK.Http;
using CQ.AuthProvider.SDK.Sessions;

namespace CQ.AuthProvider.SDK.Accounts;

internal sealed class AccountService(AuthProviderClient authProviderWebApi)
: IAccountService
{
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
}