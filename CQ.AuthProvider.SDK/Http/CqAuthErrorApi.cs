using System.Net;

namespace CQ.AuthProvider.SDK.Http;

public sealed record CqAuthErrorApi
{
    public HttpStatusCode StatusCode { get; init; }

    public string Code { get; init; } = null!;

    public string Message { get; init; } = null!;

    public string Description { get; init; } = null!;
}
