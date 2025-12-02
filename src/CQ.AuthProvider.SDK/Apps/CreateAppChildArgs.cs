namespace CQ.AuthProvider.SDK.Apps;

public readonly struct CreateAppChildArgs()
{
    public string Name { get; init; } = null!;
    public Logo Logo { get; init; } = null!;
    public Background? Background { get; init; } = null!;
}
