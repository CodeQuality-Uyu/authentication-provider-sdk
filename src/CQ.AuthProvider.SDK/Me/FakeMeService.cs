using CQ.AuthProvider.SDK.Accounts;

namespace CQ.AuthProvider.SDK.Me;

internal sealed class FakeMeService : IMeService
{
    public Task<AccountLogged> GetAsync(string token)
    {
        var fakeAccount = new AccountLogged
        {
            Id = Guid.NewGuid(),
            Email = "email@fake.com"
        };

        return Task.FromResult(fakeAccount);
    }

    public Task UpdateAsync(
        UpdateMeArgs args,
        AccountLogged accountLogged)
    {
        return Task.CompletedTask;
    }
}