using CQ.AuthProvider.SDK.Accounts;
using CQ.AuthProvider.SDK.Tokens;

namespace CQ.AuthProvider.SDK.Sessions;

public sealed record SessionCreated
{
    public Guid Id { get; init; }

    public BlobRead? ProfilePicture { get; init; }

    public string Email { get; init; } = null!;

    public string FirstName { get; init; } = null!;

    public string LastName { get; init; } = null!;

    public string FullName { get; init; } = null!;

    public SessionAppLogged AppLogged { get; init; } = null!;

    public string Token { get; init; } = null!;

    /// <summary>
    /// Which format <see cref="Token"/> came in, so the client knows how to
    /// treat it.
    /// </summary>
    public TokenFormat TokenFormat { get; init; }

    /// <summary>
    /// Seconds <see cref="Token"/> remains valid. Null on an opaque token,
    /// which does not expire.
    /// </summary>
    public int? ExpiresIn { get; init; }

    /// <summary>
    /// Token used to get a new access token at POST /sessions/refresh. Null on
    /// an opaque session, which has nothing to refresh.
    /// </summary>
    public string? RefreshToken { get; init; }

    public IList<string> Permissions { get; init; } = [];

    public IList<string> Roles { get; init; } = [];
}

public sealed record SessionAppLogged
{
    public Guid Id { get; init; }

    public string Name { get; init; } = null!;
}