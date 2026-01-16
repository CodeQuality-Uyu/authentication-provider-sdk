using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace CQ.AuthProvider.SDK.Health;

internal sealed class FakeHealthService : IHealthService
{
    public Task<HealthCheckResult> GetAsync()
    {
        var healthyResult = HealthCheckResult.Healthy();

        return Task.FromResult(healthyResult);
    }
}