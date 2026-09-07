using System.Text.Json.Serialization;

namespace CQ.AuthProvider.SDK.Tokens;

/// <summary>
/// Which token service of the auth provider minted an access token. Both
/// formats travel under the same "Bearer" scheme, so this is the only thing
/// that tells the client how to treat the token it just got.
/// </summary>
/// <remarks>
/// Serialized by name rather than by ordinal, so it reads as "Jwt" on the wire.
/// The converter is declared here instead of globally on purpose: turning it on
/// for every enum would also rewrite <c>ErrorResponse.StatusCode</c> from 401 to
/// "Unauthorized" and break every error consumer.
/// </remarks>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TokenFormat
{
    /// <summary>
    /// Opaque guid backed by a session row in the auth provider. The default: a
    /// client that says nothing keeps getting exactly what it got before JWT
    /// existed. Cannot be read by the client, does not expire, has no refresh
    /// token, and validating it means calling the provider.
    /// </summary>
    Opaque,

    /// <summary>
    /// Self contained RS256 JWT. Opt in: it has to be asked for. Its claims can
    /// be read and its signature validated against the provider's
    /// /.well-known/jwks.json, it expires, and it comes with a refresh token.
    /// </summary>
    Jwt
}
