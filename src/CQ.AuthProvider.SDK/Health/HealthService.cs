
using CQ.AuthProvider.SDK.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace CQ.AuthProvider.SDK.Health;

internal sealed class HealthService(AuthProviderClient authProviderWebApi)
: IHealthService
{
    public async Task<HealthCheckResult> GetAsync()
    {
        var response = await authProviderWebApi
        .GetAsync<HealthResponse>("health")
        .ConfigureAwait(false);


        if (response.Status == "Healthy")
        {
            return HealthCheckResult.Healthy();
        }

        return HealthCheckResult.Unhealthy();
    }
}