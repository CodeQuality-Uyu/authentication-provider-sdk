using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK
{
    internal sealed record class CheckPermissionResult
    {
        public bool HasPermission { get; init; }
    }
}
