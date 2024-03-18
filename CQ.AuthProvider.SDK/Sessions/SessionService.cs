using CQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK.Sessions
{
    internal class SessionService : ISessionService
    {
        private readonly AuthProviderApi _cqAuthApi;

        public SessionService(AuthProviderApi cqAuthApi)
        {
            _cqAuthApi = cqAuthApi;
        }

        /// <summary>
        /// Create a session for the user with those credentials
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <exception cref="RequestException<CqAuthErrorApi>"></exception>"
        public async Task<SessionCreated> LoginAsync(CreateSessionPassword sessionPassword)
        {
            var successBody = await _cqAuthApi.PostAsync<SessionResponse>(
                "sessions/credentials",
                sessionPassword)
                .ConfigureAwait(false);

            return new SessionCreated(
                successBody.AccountId,
                successBody.Email,
                successBody.Token,
                successBody.Roles,
                successBody.Permissions);
        }

        public async Task<bool> IsTokenValidAsync(string token)
        {
            var successBody = await this._cqAuthApi
                .GetAsync<TokenValidationResponse>($"sessions/{token}/validate")
                .ConfigureAwait(false);

            return successBody.IsValid;
        }
    }
}
