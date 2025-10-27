namespace CQ.AuthProvider.SDK.Abstractions.Users;
public interface IUserService
{
    Task<object> GetUserOfAccountAsync(string accountId);
}
