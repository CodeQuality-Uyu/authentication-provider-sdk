namespace CQ.AuthProvider.SDK.Http;

public sealed record AuthProviderSection
{
    public const string Name = "Authentication";

    public string Server { get; init; } = null!;

    public string? SubscriptionKey { get; init; } = null!;
}
