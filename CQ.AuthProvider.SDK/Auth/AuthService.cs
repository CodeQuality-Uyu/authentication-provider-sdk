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
        private readonly HttpClientAdapter _cqAuthApi;

        public AuthService(HttpClientAdapter cqAuthApi)
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
                ProcessAuthCredentialsErrorBody(auth, errorResponse);
            };

            var successBody = await _cqAuthApi.PostAsync<Auth, CqAuthErrorApi>("auth/credentials", auth, processError).ConfigureAwait(false);

            return successBody;
        }

        private void ProcessAuthCredentialsErrorBody(CreateAuthPassword body, CqAuthErrorApi error)
        {
            if (error.AuthCode == CqAuthErrorCode.DuplicatedEmail) throw new DuplicatedEmailException(body.Email);
        }
    }
}
