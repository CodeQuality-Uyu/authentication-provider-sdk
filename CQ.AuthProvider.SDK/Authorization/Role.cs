using CQ.AuthProvider.SDK.Accounts;
using CQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK.Authorization
{
    public sealed record class Role
    {
        public string Name { get; init; }

        public string Description { get; init; }

        public RoleKey Key { get; init; }

        public List<PermissionKey> Permissions { get; init; }

        public Role(
            string name,
            string description,
            RoleKey key,
            List<PermissionKey> permissions)
        {
            this.Name = Guard.Encode(name);
            this.Description = Guard.Encode(description);

            this.Key = key;
            this.Permissions = permissions;
        }
    }
}
