using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK
{
    public interface IMeService
    {
        Task<Auth> GetAsync(string token);

        Task<bool> HasPermissionAsync(string permission, string token);
    }
}
