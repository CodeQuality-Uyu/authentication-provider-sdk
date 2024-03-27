using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK.Authorization
{
    internal sealed class CreatePermissionBulkRequest
    {
        public List<CreatePermissionRequest> Permissions { get; init; } = null!;
    }

    internal sealed class CreatePermissionRequest
    {
        public string Name { get; init; } = null!;

        public string Description { get; init; } = null!;

        public string Key { get; init; } = null!;
    }
}
