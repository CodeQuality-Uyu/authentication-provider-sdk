
namespace CQ.AuthProvider.SDK.ApiFilters;

public sealed class CqAuthException : Exception
{
    public readonly string ErrorCode;

    public CqAuthException(string errorCode, string message) : base(message)
    {
        ErrorCode = errorCode;
    }
}
