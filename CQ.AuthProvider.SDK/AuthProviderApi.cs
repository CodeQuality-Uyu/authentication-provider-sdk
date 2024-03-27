using CQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK
{
    public sealed class AuthProviderApi : ConcreteHttpClient<CqAuthErrorApi>
    {
        public AuthProviderApi(string baseUrl) : base(baseUrl) { }

        public AuthProviderApi(HttpClient client) : base(client) { }

        protected override Exception? ProcessError(CqAuthErrorApi error)
        {
            return new CqAuthException(error.AuthCode, error.Message);
        }
    }
}
