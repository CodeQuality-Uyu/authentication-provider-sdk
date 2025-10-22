namespace CQ.AuthProvider.SDK.Sessions;

public sealed record SessionCreated
{
    public Guid Id { get; init; }

    public string? ProfilePicture { get; init; }

    public string Email { get; init; } = null!;

    public string FirstName { get; init; } = null!;

    public string LastName { get; init; } = null!;

    public string FullName { get; init; } = null!;

    public SessionAppLogged AppLogged { get; init; } = null!;

    public string Token { get; init; } = null!;

    public IList<string> Permissions { get; init; } = [];

    public IList<string> Roles { get; init; } = [];
}

public sealed record SessionAppLogged
{
    public Guid Id { get; init; }

    public string Name { get; init; } = null!;
}