using CQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK
{
    public sealed class AuthService : IAuthService
    {
        private readonly AuthProviderApi _cqAuthApi;

        public AuthService(AuthProviderApi cqAuthApi)
        {
            _cqAuthApi = cqAuthApi;
        }

        /// <summary>
        /// Creats an auth account in cq auth provider
        /// </summary>
        /// <param name="auth"></param>
        /// <returns></returns>
        /// <exception cref="DuplicatedEmailException"></exception>"
        /// <exception cref="RequestException<CqAuthErrorApi>"></exception>"
        public async Task<AuthCreated> CreateAsync(CreateAuthPassword auth)
        {
            var successBody = await _cqAuthApi.PostAsync<Auth>("auths/credentials", auth).ConfigureAwait(false);

            return new AuthCreated(successBody.Id, successBody.Email, successBody.Token, successBody.Roles);
        }
    }
}
