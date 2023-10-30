using CQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK
{
    public class AuthProviderApi : HttpClientAdapter
    {
        public AuthProviderApi() { }

        public AuthProviderApi(string baseUrl) : base(baseUrl) { }
    }
}
