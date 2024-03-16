using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK.Accounts
{
    public sealed record class AccountResult
    {

        public readonly string Id;

        public readonly string FullName;

        public readonly string FirstName;
        
        public readonly string LastName;

        public readonly string Email;

        public readonly List<RoleKey> Roles;

        public readonly List<PermissionKey> Permissions;

        public AccountResult(
            string id,
            string fullName,
            string firstName,
            string lastName,
            string email,
            List<string> roles,
            List<string> permissions)
        {
            Id = id;
            FullName = fullName;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Roles = roles.Select(r => new RoleKey(r)).ToList();
            Permissions = permissions.Select(p => new PermissionKey(p)).ToList();
        }
    }
}
