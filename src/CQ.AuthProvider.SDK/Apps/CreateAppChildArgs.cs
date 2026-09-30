namespace CQ.AuthProvider.SDK.Apps;

public readonly struct CreateAppChildArgs()
{
    public required string Name { get; init; } = null!;

    public CreateLogoArgs? Logo { get; init; } = null!;
}

public sealed record CreateLogoArgs
{
    public required string ColorKey { get; init; }

    public required string LightKey { get; init; }

    public required string DarkKey { get; init; }
}