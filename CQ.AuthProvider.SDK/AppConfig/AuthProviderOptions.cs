using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK.AppConfig
{
    public sealed record class AuthProviderOptions
    {
        public const string AuthProvider = "Authentication";

        public string Server { get; init; } = null!;

        public string PrivateKey {  get; init; } = null!;

        public FakeOptions Fake { get; init; } = null!;
    }

    public sealed record class FakeOptions
    {
        public bool IsActive { get; init; }

        public string FirstName { get; init; } = null!;
        
        public string LastName { get; init; } = null!;
        
        public string FullName{ get; init; } = null!;
        
        public string Email { get; init; } = null!;
    }
}
