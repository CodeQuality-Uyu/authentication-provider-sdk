using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace CQ.AuthProvider.SDK.Health;

internal sealed class FakeAuthProviderServiceHealthCheck
: IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        return HealthCheckResult.Healthy("Fake Auth Provider is always healthy.");
    }
}