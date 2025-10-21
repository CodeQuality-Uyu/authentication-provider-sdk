
using CQ.AuthProvider.SDK.Http;

namespace CQ.AuthProvider.SDK.Health;

internal sealed class HealthService(AuthProviderConnectionApi _authProviderWebApi)
: IHealthService
{
    public async Task<bool> IsAliveAsync()
    {
        var response = await _authProviderWebApi
        .GetAsync<AuthHealth>("health")
        .ConfigureAwait(false);

        return response.Alive;
    }
}