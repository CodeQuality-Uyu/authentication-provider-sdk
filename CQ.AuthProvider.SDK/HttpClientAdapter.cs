using CQ.AuthProvider.SDK.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK
{
    public class HttpClientAdapter
    {
        private readonly HttpClient _httpClient;

        public HttpClientAdapter()
        {
            _httpClient = new();
        }

        public HttpClientAdapter(string url)
        {
            _httpClient = new()
            {
                BaseAddress = new Uri(url),
            };
        }

        public virtual async Task<TSuccessBody> PostAsync<TSuccessBody>(string uri, object value, Action<CqAuthErrorApi>? processError = null)
            where TSuccessBody : class
        {
            var response = await _httpClient.PostAsJsonAsync(uri, value).ConfigureAwait(false);

            return await ProcessResponseAsync<TSuccessBody>(response, processError).ConfigureAwait(false);
        }

        private async Task<TSuccessBody> ProcessResponseAsync<TSuccessBody>(HttpResponseMessage response, Action<CqAuthErrorApi>? processErrorResponse = null)
            where TSuccessBody : class
        {
            if (!response.IsSuccessStatusCode)
            {
                var errorBody = await this.ProcessBodyAsync<CqAuthErrorApi>(response).ConfigureAwait(false);

                processErrorResponse?.Invoke(errorBody);

                throw new CqAuthServiceException(errorBody);
            }

            var successBody = await this.ProcessBodyAsync<TSuccessBody>(response).ConfigureAwait(false);

            return successBody;
        }

        private async Task<TBody> ProcessBodyAsync<TBody>(HttpResponseMessage response)
            where TBody : class
        {
            return await response.Content.ReadFromJsonAsync<TBody>().ConfigureAwait(false);
        }
    }
}
