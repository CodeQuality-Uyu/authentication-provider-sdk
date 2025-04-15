
namespace CQ.AuthProvider.SDK.Accounts;

public readonly struct BlobRead()
{
    public Guid Id { get; init; }

    public string Key { get; init; } = null!;

    public string ReadUrl { get; init; } = null!;
}
