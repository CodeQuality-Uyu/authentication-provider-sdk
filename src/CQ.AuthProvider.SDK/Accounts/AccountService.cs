
using CQ.AuthProvider.SDK.Http;

namespace CQ.AuthProvider.SDK.Accounts;

internal sealed class AccountService(AuthProviderConnectionApi _authProviderWebApi)
: IAccountService
{
    public async Task<AccountCreated> CreateAsync(CreateAccountPasswordArgs args)
    {
        var response = await _authProviderWebApi
            .PostAsync<AccountCreated>("accounts/credentials",args,[])
            .ConfigureAwait(false);

        return response;
    }
}