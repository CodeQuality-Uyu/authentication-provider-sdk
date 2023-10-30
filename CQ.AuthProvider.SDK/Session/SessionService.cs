using CQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK
{
    public class SessionService : ISessionService
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
        public async Task<Session> LoginAsync(CreateSessionPassword sessionPassword)
        {
            var successBody = await _cqAuthApi.PostAsync<Session, CqAuthErrorApi>(
                "session/credentials", 
                sessionPassword, 
                (error) =>
            {
                if (error.AuthCode == CqAuthErrorCode.InvalidCredentials) throw new InvalidCredentialsException();
            })
                .ConfigureAwait(false);

            return successBody;
        }
    }
}
