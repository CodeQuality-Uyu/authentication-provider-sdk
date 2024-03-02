using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK.Accounts
{
    public sealed record class AccountCreated
    {
        public readonly string Id;

        public readonly string Name;

        public readonly string Email;

        public readonly string Token;

        public readonly List<RoleKey> Roles;

        public readonly List<PermissionKey> Permissions;

        public AccountCreated(
            string id,
            string name,
            string email,
            string token,
            List<string> roles,
            List<string> permissions)
        {
            Id = id;
            Name = name;
            Email = email;
            Token = token;
            Roles = roles.Select(r => new RoleKey(r)).ToList();
            Permissions = permissions.Select(p => new PermissionKey(p)).ToList();
        }
    }
}
