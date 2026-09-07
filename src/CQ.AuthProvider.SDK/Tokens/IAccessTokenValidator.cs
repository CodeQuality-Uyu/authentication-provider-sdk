using CQ.AuthProvider.SDK.Accounts;

namespace CQ.AuthProvider.SDK.Tokens;

public interface IAccessTokenValidator
{
    /// <summary>
    /// Builds the logged account out of the token itself, without calling the
    /// auth provider.
    /// </summary>
    /// <param name="authorizationHeaderValue">
    /// The Authorization header as it arrived, scheme included.
    /// </param>
    /// <returns>
    /// Null when the token cannot be resolved here and the auth provider has to
    /// be asked: an opaque token, a subscription key, or a JWT that arrived
    /// while the signing keys are unreachable.
    /// </returns>
    /// <exception cref="Http.CqAuthException">
    /// The token is a JWT and it is not valid. It is rejected here instead of
    /// being forwarded: the auth provider would reject it too.
    /// </exception>
    Task<AccountLogged?> GetOrDefaultAsync(string authorizationHeaderValue);
}
