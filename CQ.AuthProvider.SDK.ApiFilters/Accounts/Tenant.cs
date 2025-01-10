namespace CQ.AuthProvider.SDK.ApiFilters.Accounts;

public sealed record Tenant
{
    public Guid Id { get; init; }

    public string Name { get; init; } = null!;

    public Multimedia MiniLogo { get; init; } = null!;

    public Multimedia CoverLogo { get; init; } = null!;

    public string WebUrl { get; init; } = null!;
}
