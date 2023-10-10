using CQ.AuthProvider.SDK.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK
{
    internal class AuthService : IAuthService
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

            var successBody = await this.ProcessResponseAsync(auth, response).ConfigureAwait(false);

            return successBody;
        }

        private async Task<Auth> ProcessResponseAsync(CreatePasswordAuth request, HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var errorBody = await this.ProcessBodyAsync<CqErrorApi>(response).ConfigureAwait(false);

                this.ProcessErrorBody(request, errorBody);
            }

            var successBody = await this.ProcessBodyAsync<Auth>(response).ConfigureAwait(false);

            return successBody;
        }

        private async Task<TBody> ProcessBodyAsync<TBody>(HttpResponseMessage response)
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
    }
}
