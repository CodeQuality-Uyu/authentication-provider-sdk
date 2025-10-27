using CQ.AuthProvider.SDK.Abstractions.Sessions;
using CQ.AuthProvider.SDK.Accounts;
using CQ.AuthProvider.SDK.AppConfig;
using CQ.AuthProvider.SDK.Sessions;

namespace CQ.AuthProvider.SDK.AuthProviderConnections.Fake;
internal sealed class AuthProviderConnectionFake(AuthProviderSection authSection) :
    IAuthProviderConnection
{
    public Task<AccountLoggedResponse> CreateAccountAsync(CreateAccountPasswordRequest request)
    {
        return Task.FromResult(new AccountLoggedResponse()
        {
            Id = authSection.Fake.Account.Id,
            FirstName = authSection.Fake.Account.FirstName,
            LastName = authSection.Fake.Account.LastName,
            FullName = authSection.Fake.Account.FullName,
            Email = authSection.Fake.Account.Email,
            Token = string.Empty,
            Permissions = authSection.Fake.Account.Permissions
        });
    }

    public async Task<AccountCreatedResponse> CreateAccountForAsync(CreateAccountPasswordRequest request)
    {
        return await CreateAccountAsync(request).ConfigureAwait(false);
    }

    public Task<AccountCreatedResponse> GetByTokenAsync(string token)
    {
        return CreateAccountForAsync(null);
    }

    public Task<bool> IsActiveAsync()
    {
        return Task.FromResult(true);
    }

    public Task<bool> IsTokenValidAsync(string token)
    {
        return Task.FromResult(true);
    }

    public Task<SessionResponse> LoginAsync(CreateSessionPassword credentials)
    {
        return Task.FromResult(new SessionResponse
        {
            AccountId = authSection.Fake.Account.Id,
            Email = authSection.Fake.Account.Email,
            Token = string.Empty,
            Permissions = authSection.Fake.Account.Permissions
        });
    }
}
