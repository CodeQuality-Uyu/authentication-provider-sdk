
namespace CQ.AuthProvider.SDK.Accounts;

public sealed record BlobRead()
{
    public string Key { get; init; } = null!;

    public string Url { get; init; } = null!;
}
