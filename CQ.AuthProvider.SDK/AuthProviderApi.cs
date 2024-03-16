using CQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK
{
    public class AuthProviderApi : HttpClientAdapter
    {
        private delegate Task<TSuccessBody> AuthProviderRequestAsync<TSuccessBody>()
            where TSuccessBody : class;

        public AuthProviderApi(string baseUrl) : base(baseUrl) { }

        public AuthProviderApi(HttpClient client) : base(client) { }

        public virtual async Task<TSuccessBody> PostAsync<TSuccessBody>(string uri, object value, List<Header>? headers = null)
            where TSuccessBody : class
        {
            var request = async () =>
            {
                return await base.PostAsync<TSuccessBody, CqAuthErrorApi>(uri, value, ProcessCqAuthError, headers).ConfigureAwait(false);
            };

            var authProviderRequest = new AuthProviderRequestAsync<TSuccessBody>(request);

            return await ExecuteRequest(authProviderRequest).ConfigureAwait(false);
        }

        public virtual async Task<TSuccessBody> GetAsync<TSuccessBody>(string uri, List<Header>? headers = null)
            where TSuccessBody : class
        {
            var request = async () =>
            {
                return await base.GetAsync<TSuccessBody, CqAuthErrorApi>(uri, ProcessCqAuthError, headers).ConfigureAwait(false);
            };

            var authProviderRequest = new AuthProviderRequestAsync<TSuccessBody>(request);

            return await ExecuteRequest(authProviderRequest).ConfigureAwait(false);
        }

        private Exception? ProcessCqAuthError(CqAuthErrorApi error)
        {
            return new CqAuthException(error.AuthCode, error.Message);
        }

        private async Task<TSuccessBody> ExecuteRequest<TSuccessBody>(
            AuthProviderRequestAsync<TSuccessBody> RequestAsync)
            where TSuccessBody : class
        {
            var response = await RequestAsync().ConfigureAwait(false);

            return response;
        }
    }
}
