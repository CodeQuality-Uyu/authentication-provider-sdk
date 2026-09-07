namespace CQ.AuthProvider.SDK.Tokens;

/// <summary>
/// Local validation of the JWT access tokens issued by the auth provider. Bound
/// from "Authentication:Jwt"; every value has a default, so an app that says
/// nothing still validates JWTs against the provider's published keys.
/// </summary>
public sealed record JwtSection
{
    public const string Name = "Authentication:Jwt";

    /// <summary>
    /// When false every token, JWT or not, is resolved by calling GET /me on
    /// the provider, which is how the SDK behaved before JWT existed.
    /// </summary>
    public bool IsActive { get; init; } = true;

    /// <summary>
    /// Has to match the "Issuer" configured on the auth provider.
    /// </summary>
    public string Issuer { get; init; } = "cq-auth-provider";

    /// <summary>
    /// Where the signing keys are published. Defaults to
    /// "{Authentication:Server}/.well-known/jwks.json".
    /// </summary>
    public string? JwksUri { get; init; }

    /// <summary>
    /// Id of this app in the auth provider. When set, a token issued for
    /// another app is rejected ("aud" carries the app the account logged into).
    /// Left empty the audience is not checked, which is what an api serving
    /// several apps needs.
    /// </summary>
    public Guid? AppId { get; init; }

    public int ClockSkewInSeconds { get; init; } = 30;

    /// <summary>
    /// How long the fetched keys are reused. A rotation is picked up before
    /// this expires anyway: a token signed with an unknown key forces a refetch.
    /// </summary>
    public int KeysCacheInMinutes { get; init; } = 60;
}
