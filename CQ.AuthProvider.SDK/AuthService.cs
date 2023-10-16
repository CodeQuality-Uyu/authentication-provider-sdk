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
        private readonly HttpClient _cqAuthApi;

        public AuthService(string cqAuthApiUrl)
        {
            this._cqAuthApi = new()
            {
                BaseAddress = new Uri(cqAuthApiUrl)
            };
        }

        public async Task<Auth> CreateAsync(CreatePasswordAuth auth)
        {
            var response = await this._cqAuthApi.PostAsJsonAsync("auth/credentials",auth).ConfigureAwait(false);

            var processError = (CqErrorApi errorResponse) =>
            {
                ProcessErrorBody(auth, errorResponse);
            };

            var successBody = await this.ProcessResponseAsync<Auth>(response, processError).ConfigureAwait(false);

            return successBody;
        }

        private async Task<TSuccessBody> ProcessResponseAsync<TSuccessBody>(HttpResponseMessage response, Action<CqErrorApi>? processErrorResponse = null)
            where TSuccessBody : class
        {
            if (!response.IsSuccessStatusCode)
            {
                var errorBody = await this.ProcessBodyAsync<CqErrorApi>(response).ConfigureAwait(false);

                processErrorResponse?.Invoke(errorBody);
            }

            var successBody = await this.ProcessBodyAsync<TSuccessBody>(response).ConfigureAwait(false);

            return successBody;
        }

        private async Task<TBody> ProcessBodyAsync<TBody>(HttpResponseMessage response)
            where TBody : class
        {
            return await response.Content.ReadFromJsonAsync<TBody>().ConfigureAwait(false);
        }

        private void ProcessErrorBody(CreatePasswordAuth body, CqErrorApi error)
        {
            switch (error.Code)
            {
                case CqAuthErrorCodes.DuplicatedEmail:
                    {
                        throw new DuplicatedEmailException(body.Email);
                    }
                default:
                    throw new CqAuthServiceException();
            }
        }

        public async Task<Auth> LoginAsync(string email, string password)
        {
            var response = await this._cqAuthApi.PostAsJsonAsync("auth/login", new { email, password }).ConfigureAwait(false);

            var successBody = await this.ProcessResponseAsync<Auth>(response).ConfigureAwait(false);

            return successBody;
        }
    }
}
