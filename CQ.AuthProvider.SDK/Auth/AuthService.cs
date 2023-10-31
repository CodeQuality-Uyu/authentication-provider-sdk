using CQ.AuthProvider.SDK.Exceptions;
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
        public async Task<Auth> CreateAsync(CreateAuthPassword auth)
        {
            var processError = (CqAuthErrorApi errorResponse) =>
            {
                return ProcessAuthCredentialsErrorBody(auth, errorResponse);
            };

            var successBody = await _cqAuthApi.PostAsync<Auth>("auth/credentials", auth, processError).ConfigureAwait(false);

            return successBody;
        }

        private Exception? ProcessAuthCredentialsErrorBody(CreateAuthPassword body, CqAuthErrorApi error)
        {
            if (error.AuthCode == CqAuthErrorCode.DuplicatedEmail) return new DuplicatedEmailException(body.Email);

            return null;
        }
    }
}
