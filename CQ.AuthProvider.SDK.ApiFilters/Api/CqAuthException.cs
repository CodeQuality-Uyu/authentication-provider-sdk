using System.Net;

namespace CQ.AuthProvider.SDK.ApiFilters;

internal sealed class CqAuthException
    : Exception
{
    public HttpStatusCode StatusCode { get; init; }

    public string Code { get; init; }

    public string Description { get; init; }


    public CqAuthException(CqAuthErrorApi error)
        : base(error.Message)
    {
        StatusCode = error.StatusCode;
        Code = error.Code;
        Description = error.Description;
    }
}
