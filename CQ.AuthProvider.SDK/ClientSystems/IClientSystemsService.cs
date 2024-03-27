using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK.ClientSystems
{
    public interface IClientSystemsService
    {
        Task<ClientSystem> GetByPrivateKeyAsync(string privateKey);
    }
}
