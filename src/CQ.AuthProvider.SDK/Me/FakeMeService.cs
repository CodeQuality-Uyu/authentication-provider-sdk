using System.Security.Principal;
using CQ.AuthProvider.SDK.Accounts;
using Microsoft.Extensions.Options;

namespace CQ.AuthProvider.SDK.Me;

internal sealed class FakeMeService(IOptions<IPrincipal> accountLoggedOption) : IMeService
{
    private readonly IPrincipal _accountLogged = accountLoggedOption.Value;

    public Task<AccountLogged> GetAsync(string token)
    {
        var fakeAccount = (AccountLogged)_accountLogged;
        
        return Task.FromResult(fakeAccount);
    }

    public Task UpdateAsync(
        UpdateMeArgs args,
        AccountLogged accountLogged)
    {
        return Task.CompletedTask;
    }
}