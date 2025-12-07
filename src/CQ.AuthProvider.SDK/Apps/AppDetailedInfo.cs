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
    public string? ColorConfig { get; init; }

    public string? ColorKey { get; init; }
}

public sealed record Logo
{
    public string ColorKey { get; init; } = null!;

    public string LightKey { get; init; } = null!;

    public string DarkKey { get; init; } = null!;
}