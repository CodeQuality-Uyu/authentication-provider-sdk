using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK.Accounts
{
    internal sealed record class AccountResponse
    {
        public string Id { get; init; } = null!;

        public string FullName { get; init; } = null!;

        public string FirstName { get; init; } = null!;

        public string LastName { get; init; } = null!;

        public string Email { get; init; } = null!;

        public List<string> Roles { get; init; } = null!;
        
        public List<string> Permissions { get; init; } = null!;
    }
}
