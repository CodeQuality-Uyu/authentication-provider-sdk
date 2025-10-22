
using CQ.AuthProvider.SDK.Http;

namespace CQ.AuthProvider.SDK.Accounts;

internal sealed class AccountService(AuthProviderConnectionApi authProviderWebApi)
: IAccountService
{
    public async Task<AccountCreated> CreateAsync(CreateAccountPasswordArgs args)
    {
        var response = await authProviderWebApi
            .PostAsync<AccountCreated>("accounts/credentials", args, [])
            .ConfigureAwait(false);

        return response;
    }
}