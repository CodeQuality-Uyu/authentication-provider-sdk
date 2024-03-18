using CQ.AuthProvider.SDK.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK.Sessions
{
    public sealed record class SessionCreated
    {
        public readonly string AccountId;

        public readonly string Email;

        public readonly string Token;

        public readonly List<RoleKey> Roles;

        public readonly List<PermissionKey> Permissions;

        internal SessionCreated(
            string accountId,
            string email,
            string token,
            List<string> roles,
            List<string> permissions)
        {
            AccountId = accountId;
            Email = email;
            Token = token;
            Roles = roles.Select(r => new RoleKey(r)).ToList();
            Permissions = permissions.Select(p => new PermissionKey(p)).ToList();
        }
    }
}
