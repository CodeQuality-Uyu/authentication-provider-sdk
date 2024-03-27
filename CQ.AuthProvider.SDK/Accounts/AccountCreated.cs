using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK.Accounts
{
    public sealed record class AccountCreated : Account
    {
        public string Token { get; init; } = null!;
    }
}
