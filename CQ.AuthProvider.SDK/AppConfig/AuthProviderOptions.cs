using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK.AppConfig
{
    public sealed record class AuthProviderOptions
    {
        public string AuthProviderApiUrl { get; init; } = null!;

        public string PrivateKey {  get; init; } = null!;
    }
}
