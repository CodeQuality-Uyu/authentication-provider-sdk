
namespace CQ.AuthProvider.SDK.ApiFilters.Accounts;

public sealed record Multimedia
{
    public Guid Id { get; init; }

    public string ReadUrl { get; init; } = null!;

    public string WriteUrl { get; init; } = null!;
}
