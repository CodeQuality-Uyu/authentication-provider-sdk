namespace CQ.AuthProvider.SDK.Apps;

public readonly struct CreateAppChildArgs()
{
    public required string Name { get; init; } = null!;

    public required Logo Logo { get; init; } = null!;

    public Background? Background { get; init; } = null!;
}
