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
        private delegate Task<TSuccessBody> AuthProviderRequestAsync<TSuccessBody, TErrorBody>(string uri, Func<CqAuthErrorApi, Exception?> processError, IList<Header>? headers = null) where TSuccessBody : class where TErrorBody : class;

        public AuthProviderApi() { }

        public AuthProviderApi(string baseUrl) : base(baseUrl) { }

        public virtual async Task<TSuccessBody> PostAsync<TSuccessBody>(string uri, object value, IList<Header>? headers = null)
            where TSuccessBody : class
        {
            var request = async (string uri, Func<CqAuthErrorApi, Exception?> processError, IList<Header>? headers) =>
            {
                return await base.PostAsync<TSuccessBody, CqAuthErrorApi>(uri, value, processError, headers).ConfigureAwait(false);
            };

            var authProviderRequest = new AuthProviderRequestAsync<TSuccessBody, CqAuthErrorApi>(request);

            return await ExecuteRequest<TSuccessBody, CqAuthErrorApi>(uri, authProviderRequest, headers).ConfigureAwait(false);
        }


        public virtual async Task<TSuccessBody> GetAsync<TSuccessBody>(string uri, IList<Header>? headers = null)
            where TSuccessBody : class
        {
            return await ExecuteRequest<TSuccessBody, CqAuthErrorApi>(uri, base.GetAsync<TSuccessBody, CqAuthErrorApi>, headers).ConfigureAwait(false);
        }

        private Exception? ProcessCqAuthError(CqAuthErrorApi error)
        {
            return new CqAuthException(error.AuthCode, error.Message);
        }


        private async Task<TSuccessBody> ExecuteRequest<TSuccessBody, TErrorBody>(
            string uri,
            AuthProviderRequestAsync<TSuccessBody, TErrorBody> Request,
            IList<Header>? headers = null)
            where TSuccessBody : class
            where TErrorBody : class
        {
            var response = await Request(uri, ProcessCqAuthError, headers).ConfigureAwait(false);

            return response;
        }
    }
}
