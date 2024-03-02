using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK.HealthChecks
{
    public interface IAuthHealthService
    {
        Task<bool> IsActiveAsync();
    }
}
