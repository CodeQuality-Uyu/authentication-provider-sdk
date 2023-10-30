using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK
{
    public sealed record class Session
    {
        public string AuthId { get; init; }

        public string Email { get; init; }

        public string? Name { get; init; }

        public string Token { get; init; }
    }
}
