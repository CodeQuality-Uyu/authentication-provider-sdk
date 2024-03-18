using CQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK.Accounts
{
    public sealed record class RoleKey
    {
        private readonly string Value;

        public RoleKey(string value)
        {
            Value = Guard.Encode(value, nameof(value));
        }

        public override string ToString()
        {
            return this.Value;
        }
    }
}
