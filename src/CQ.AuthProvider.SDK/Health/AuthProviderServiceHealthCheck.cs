using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace CQ.AuthProvider.SDK.Health;

internal sealed class AuthProviderServiceHealthCheck(IHealthService healthService)
: IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        var result = await healthService
            .GetAsync()
            .ConfigureAwait(false);

        return result;
    }
}