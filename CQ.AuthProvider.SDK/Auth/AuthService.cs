using CQ.AuthProvider.SDK.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK
{
    public class AuthService : IAuthService
    {
        private readonly HttpClientAdapter _cqAuthApi;

        public AuthService(HttpClientAdapter cqAuthApi)
        {
            _cqAuthApi = cqAuthApi;
        }

        public async Task<Auth> CreateAsync(CreatePasswordAuth auth)
        {
            var processError = (CqAuthErrorApi errorResponse) =>
            {
                ProcessAuthCredentialsErrorBody(auth, errorResponse);
            };

            var successBody = await _cqAuthApi.PostAsync<Auth>("auth/credentials", auth, processError).ConfigureAwait(false);

            return successBody;
        }

        private void ProcessAuthCredentialsErrorBody(CreatePasswordAuth body, CqAuthErrorApi error)
        {
            switch (error.Code)
            {
                case CqAuthErrorCodes.DuplicatedEmail:
                    {
                        throw new DuplicatedEmailException(body.Email);
                    }
            }
        }
    }
}
