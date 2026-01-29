using CQ.AuthProvider.SDK.Sessions;

namespace CQ.AuthProvider.SDK.Accounts;

internal sealed class FakeAccountService : IAccountService
{
    public Task<SessionCreated> CreateAsync(CreateAccountPasswordArgs args)
    {
        var fakeAccount = new SessionCreated
        {
            Id = Guid.NewGuid(),
        };

        return Task.FromResult(fakeAccount);
    }

    public Task<AccountCreated> CreateForAsync(CreateAccountForArgs args, AccountLogged accountLogged)
    {
        var fakeAccount = new AccountCreated
        {
            Id = Guid.NewGuid(),
        };

        return Task.FromResult(fakeAccount);
    }

    public Task<AccountCreated> CreateForWithSubscriptionAsync(CreateAccountForArgs args)
    {
        var fakeAccount = new AccountCreated
        {
            Id = Guid.NewGuid(),
        };

        return Task.FromResult(fakeAccount);
    }
}