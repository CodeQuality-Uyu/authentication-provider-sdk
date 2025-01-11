
namespace CQ.AuthProvider.SDK.ApiFilters;

public sealed class CqAuthException : Exception
{
    public readonly string Code;

    public readonly string Description;

    public CqAuthException(CqAuthErrorApi error) : base(error.Message)
    {
        Code = error.Code;
        Description = error.Description;
    }
}
