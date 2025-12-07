using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace CQ.AuthProvider.SDK.Health;

public interface IHealthService
{
    Task<HealthCheckResult> GetAsync();
}