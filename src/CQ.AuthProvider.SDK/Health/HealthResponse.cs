namespace CQ.AuthProvider.SDK.Health;

internal sealed record HealthResponse
{
    public string Status { get; init; } = null!;
}