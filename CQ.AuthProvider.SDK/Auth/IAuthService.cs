namespace CQ.AuthProvider.SDK
{
    public interface IAuthService
    {
        Task<Auth> CreateAsync(CreatePasswordAuth auth);
    }
}