using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK.Accounts
{
    internal sealed record class CheckPermissionResponse
    {
        public bool HasPermission { get; init; }
    }
}
