using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK.HealthChecks
{
    internal sealed class HealthService : IAuthHealthService
    {
        private readonly AuthProviderApi _authProviderApi;

        public HealthService(AuthProviderApi authProviderApi)
        {
            _authProviderApi = authProviderApi;
        }

        public async Task<bool> IsActiveAsync()
        {
            try
            {
                var response = await _authProviderApi.GetAsync<HealthResponse>("health").ConfigureAwait(false);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
