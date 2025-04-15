namespace CQ.AuthProvider.SDK.Health;

public interface IHealthService
{
    Task<bool> IsAliveAsync();
}