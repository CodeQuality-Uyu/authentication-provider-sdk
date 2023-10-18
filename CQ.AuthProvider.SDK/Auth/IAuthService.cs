namespace CQ.AuthProvider.SDK
{
    public interface IAuthService
    {
        Task<Auth> CreateAsync(CreateAuthPassword auth);
    }
}