namespace CQ.AuthProvider.SDK.AuthProviderConnections.Api;
public sealed record class CqAuthErrorApi
{
    public string Code { get; init; } = null!;

    public CqAuthErrorCode AuthCode => new(Code);

    public string Message { get; init; } = null!;
}
