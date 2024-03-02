using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK.Accounts
{
    public interface IMeService
    {
        Task<AccountResult> GetAsync(string token);

        Task<bool> HasPermissionAsync(PermissionKey permission, string token);
    }
}
