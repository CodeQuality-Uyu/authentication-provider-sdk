using CQ.AuthProvider.SDK.Accounts;
using CQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK.Authorization
{
    public sealed record class Permission
    {
        public string Name { get; init; }

        public string Description { get; init; }

        public PermissionKey Key { get; init; }

        public bool IsPublic { get; init; }

        public Permission(
            string name,
            string description,
            PermissionKey key,
            bool isPublic = false)
        {
            this.Name = Guard.Encode(name ?? string.Empty, nameof(Name));
            this.Description = Guard.Encode(description ?? string.Empty, nameof(Description));
            this.IsPublic = isPublic;
            this.Key = key;
        }
    }
}
