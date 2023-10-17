using CQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK
{
    public class SessionService
    {
        private readonly HttpClientAdapter _cqAuthApi;

        public SessionService(HttpClientAdapter cqAuthApi)
        {
            _cqAuthApi = cqAuthApi;
        }

        public async Task<Session> LoginAsync(string email, string password)
        {
            var successBody = await _cqAuthApi.PostAsync<Session, CqAuthErrorApi>("session/credentials", new { email, password }).ConfigureAwait(false);

            return successBody;
        }
    }
}
