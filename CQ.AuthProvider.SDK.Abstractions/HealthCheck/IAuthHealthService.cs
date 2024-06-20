namespace CQ.AuthProvider.SDK.Abstractions.HealthCheck;
public interface IAuthHealthService
{
    Task<bool> IsActiveAsync();
}
