namespace CQ.AuthProvider.SDK.Accounts;

internal sealed class FakeAccountService : IAccountService
{
    public Task<AccountCreated> CreateAsync(CreateAccountPasswordArgs args)
    {
        var fakeAccount = new AccountCreated
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
}