using CQ.AuthProvider.SDK.Accounts;

namespace CQ.AuthProvider.SDK.Tokens;

/// <summary>
/// With the fake authentication on there is no token to read: the account comes
/// from configuration, so every token is left to <c>FakeMeService</c>.
/// </summary>
internal sealed class FakeAccessTokenValidator
    : IAccessTokenValidator
{
    public Task<AccountLogged?> GetOrDefaultAsync(string authorizationHeaderValue)
    {
        return Task.FromResult<AccountLogged?>(null);
    }
}
