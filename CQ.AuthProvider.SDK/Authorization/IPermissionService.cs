using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK.Authorization
{
    internal interface IPermissionService
    {
        Task AddBulkAsync(List<Permission> permissions);
    }
}
