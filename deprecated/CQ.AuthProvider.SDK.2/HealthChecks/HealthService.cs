
using CQ.AuthProvider.SDK.Abstractions.HealthCheck;
using CQ.AuthProvider.SDK.AuthProviderConnections;

namespace CQ.AuthProvider.SDK.HealthChecks;
internal sealed class HealthService(IAuthProviderConnection _authProviderConnection) :
    IAuthHealthService
{
    public async Task<bool> IsActiveAsync()
    {
        try
        {
            var response = await _authProviderConnection
                .IsActiveAsync()
                .ConfigureAwait(false);

            return response;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
