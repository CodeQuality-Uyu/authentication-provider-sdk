using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK.Authorization
{
    public interface IRoleService
    {
        Task AddBulkAsync(List<Role> roles);

        Task AddBulkAsync(params Role[] roles);
    }
}
