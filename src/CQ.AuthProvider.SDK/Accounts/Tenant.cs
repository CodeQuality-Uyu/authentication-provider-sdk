namespace CQ.AuthProvider.SDK.Accounts;

public readonly struct Tenant()
{
    public Guid Id { get; init; }

    public string Name { get; init; } = null!;

    public BlobRead? MiniLogo { get; init; }

    public BlobRead? CoverLogo { get; init; }

    public string? WebUrl { get; init; } = null!;
}
