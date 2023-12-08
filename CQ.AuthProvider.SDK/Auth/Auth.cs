using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK
{
    public sealed record class Auth
    {
        public string Id { get; init; } = null!;

        public string Email { get; init; } = null!;

        public string Token { get; init; } = null!;

        public IList<string> Roles { get; init; } = null!;
    }
}
