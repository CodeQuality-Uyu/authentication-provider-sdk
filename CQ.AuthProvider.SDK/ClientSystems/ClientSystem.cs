using CQ.AuthProvider.SDK.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK.ClientSystems
{
    public sealed record class ClientSystem
    {
        public string Id { get; init; } = null!;

        public string Name { get; init; } = null!;

        public RoleKey Role { get; init; } = null!;

        public List<PermissionKey> Permissions { get; init; } = null!;

        public bool HasPermission(PermissionKey permission)
        {
            return Permissions.Contains(permission);
        }
    }
}
