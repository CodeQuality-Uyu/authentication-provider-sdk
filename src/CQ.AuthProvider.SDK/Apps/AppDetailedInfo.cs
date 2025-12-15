using CQ.AuthProvider.SDK.Accounts;

namespace CQ.AuthProvider.SDK.Apps;

public sealed record AppDetailedInfo
{
    public Guid Id { get; init; }

    public string Name { get; init; } = null!;

    public Logo Logo { get; init; } = null!;

    public Background? Background { get; init; }
}

public sealed record Background
{
    public BlobRead? Image { get; init; }

    public IList<string> Colors { get; init; } = [];

    public string? Config { get; init; }
}

public sealed record Logo
{
    public BlobRead Color { get; init; } = null!;

    public BlobRead Light { get; init; } = null!;

    public BlobRead Dark { get; init; } = null!;
}