using CQ.AuthProvider.SDK.Accounts;
using CQ.Utility;

namespace CQ.AuthProvider.SDK.Authorization
{
    public sealed record class Role
    {
        public string Name { get; init; }

        public string Description { get; init; }

        public RoleKey Key { get; init; }

        public List<PermissionKey> Permissions { get; init; }

        public bool IsPublic { get; init; }

        public bool IsDefault { get; init; }

        public Role(
            string name,
            string description,
            RoleKey key,
            List<PermissionKey> permissions,
            bool isPublic = false,
            bool isDefault = false)
        {
            this.Name = Guard.Encode(name, nameof(name));
            this.Description = Guard.Encode(description, nameof(description));

            this.Key = key;
            this.Permissions = permissions;
            this.IsPublic = isPublic;
            this.IsDefault = isDefault;
        }
    }
}
