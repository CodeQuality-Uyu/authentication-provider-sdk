using Microsoft.IdentityModel.Tokens;

namespace CQ.AuthProvider.SDK.Tokens;

/// <summary>
/// Public keys published by the auth provider at /.well-known/jwks.json, kept
/// in memory so validating a token does not hit the network.
/// </summary>
public interface IJwksProvider
{
    /// <summary>
    /// Keys accepted when validating an incoming access token. Empty while the
    /// endpoint cannot be reached, which is the caller's signal to fall back to
    /// the provider instead of rejecting the token.
    /// </summary>
    /// <param name="forceRefresh">
    /// Refetch even if the cached set has not expired. For the one case that
    /// cannot wait for the ttl: a token signed with a key that is not in the
    /// set, which is what a rotation looks like from here. Throttled, so a
    /// burst of bad tokens cannot turn into a burst of requests.
    /// </param>
    Task<IReadOnlyList<SecurityKey>> GetAsync(bool forceRefresh = false);
}
