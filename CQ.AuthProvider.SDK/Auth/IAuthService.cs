namespace CQ.AuthProvider.SDK
{
    public interface IAuthService
    {
        Task<AuthCreated> CreateAsync(CreateAuthPassword auth);
    }
}