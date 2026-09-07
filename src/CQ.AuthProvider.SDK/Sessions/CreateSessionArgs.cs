using CQ.AuthProvider.SDK.Tokens;

namespace CQ.AuthProvider.SDK.Sessions;

public readonly struct CreateSessionArgs()
{
    public required string Email { get; init; }

    public required string Password { get; init; }

    public Guid AppId { get; init; }

    /// <summary>
    /// Format of the access token to be issued. Left alone the auth provider
    /// answers with the opaque token of always. Asking for
    /// <see cref="TokenFormat.Jwt"/> is accepting that the token expires: that
    /// client has to handle the 401 and refresh with
    /// <see cref="SessionCreated.RefreshToken"/>.
    /// </summary>
    public TokenFormat TokenFormat { get; init; }
}