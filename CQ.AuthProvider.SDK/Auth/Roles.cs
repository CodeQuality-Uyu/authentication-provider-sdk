using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK
{
    public sealed record class Roles
    {
        private readonly string Value;

        public Roles(string value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return this.Value;
        }
    }
}
