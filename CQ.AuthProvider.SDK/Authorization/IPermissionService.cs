using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK.Authorization
{
    public interface IPermissionService
    {
        Task AddBulkAsync(List<Permission> permissions);

        Task AddBulkAsync(params Permission[] permissions);
    }
}
