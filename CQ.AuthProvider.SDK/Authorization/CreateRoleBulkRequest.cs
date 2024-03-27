using CQ.AuthProvider.SDK.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK.Authorization
{
    internal sealed class CreateRoleBulkRequest
    {
        public List<CreateRoleRequest> Roles { get; init; } = null!;
    }

    internal sealed class CreateRoleRequest
    {
        public string Name { get; init; } = null!;

        public string Description { get; init; } = null!;

        public string Key { get; init; } = null!;

        public List<string> Permissions { get; init; } = null!;

        public bool IsPublic { get; init; }

        public bool IsDefault { get; init; }
    }
}
