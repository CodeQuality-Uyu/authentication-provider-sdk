using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK.Sessions
{
    internal sealed record class TokenValidationResponse
    {
        public bool IsValid { get; init; }
    }
}
