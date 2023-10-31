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
        private delegate Task<TSuccessBody> AuthProviderRequestAsync<TSuccessBody, TErrorBody>(string uri, Func<TErrorBody, Exception?>? processError = null, IList<Header>? headers = null) where TSuccessBody : class where TErrorBody : class;

        public AuthProviderApi() { }

        public AuthProviderApi(string baseUrl) : base(baseUrl) { }

        public virtual async Task<TSuccessBody> PostAsync<TSuccessBody>(string uri, object value, Func<CqAuthErrorApi, Exception?>? processError = null, IList<Header>? headers = null)
            where TSuccessBody : class
        {
            var request = async (string uri, Func<CqAuthErrorApi, Exception?>? processError, IList<Header>? headers) =>
            {
                return await base.PostAsync<TSuccessBody, CqAuthErrorApi>(uri, value, processError, headers).ConfigureAwait(false);
            };

            var authProviderRequest = new AuthProviderRequestAsync<TSuccessBody, CqAuthErrorApi>(request);

            return await ExecuteRequest<TSuccessBody, CqAuthErrorApi>(uri, authProviderRequest, processError, headers).ConfigureAwait(false);
        }


        public virtual async Task<TSuccessBody> GetAsync<TSuccessBody>(string uri, Func<CqAuthErrorApi, Exception?>? processError = null, IList<Header>? headers = null)
            where TSuccessBody : class
        {
            return await ExecuteRequest<TSuccessBody, CqAuthErrorApi>(uri, base.GetAsync<TSuccessBody, CqAuthErrorApi>, processError, headers).ConfigureAwait(false);
        }

        private async Task<TSuccessBody> ExecuteRequest<TSuccessBody, TErrorBody>(
            string uri,
            AuthProviderRequestAsync<TSuccessBody, TErrorBody> Request,
            Func<TErrorBody, Exception?>? processError = null,
            IList<Header>? headers = null)
            where TSuccessBody : class
            where TErrorBody : class
        {
            var response = await Request(uri, processError, headers).ConfigureAwait(false);

            return response;
        }
    }
}
