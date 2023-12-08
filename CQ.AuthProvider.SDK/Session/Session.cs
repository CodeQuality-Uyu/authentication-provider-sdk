using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK
{
    internal sealed record class Session
    {
        public string AuthId { get; init; } = null!;

        public string Email { get; init; } = null!;

        public string Token { get; init; } = null!;

        public IList<string> Roles { get; init; } = null!;
    }
}
