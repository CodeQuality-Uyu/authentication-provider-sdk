using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK
{
    internal sealed record class Health
    {
        public bool IsActive { get; set; }

        public AuthProvider Auth { get; set; }
    }

    internal sealed record class AuthProvider
    {
        public string Type { get; set; }

        public bool IsActive { get; set; }
    }
}
