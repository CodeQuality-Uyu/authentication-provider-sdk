namespace CQ.AuthProvider.SDK
{
    public interface IAuthService
    {
        Task<Auth> CreateAsync(CreatePasswordAuth auth);

        Task<Auth> LoginAsync(string email, string password);
    }
}