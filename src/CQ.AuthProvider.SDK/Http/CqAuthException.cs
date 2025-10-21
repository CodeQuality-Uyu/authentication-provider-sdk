using System.Net;

namespace CQ.AuthProvider.SDK.Http;

public sealed class CqAuthException(CqAuthErrorApi error)
    : Exception(error.Message)
{
    public HttpStatusCode StatusCode { get; } = error.StatusCode;

    public string Code { get; } = error.Code;

    public string Description { get; } = error.Description;

    public object Errors { get; } = error.Errors;
}
