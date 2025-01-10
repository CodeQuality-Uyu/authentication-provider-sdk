namespace CQ.AuthProvider.SDK.ApiFilters;

public sealed record class CqAuthErrorApi
{
    public string Code { get; init; } = null!;

    public string Message { get; init; } = null!;

    public string Description { get; init; } = null!;
}
