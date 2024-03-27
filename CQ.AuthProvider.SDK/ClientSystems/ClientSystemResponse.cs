using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK.ClientSystems
{
    internal sealed class ClientSystemResponse
    {
        public string Id { get; init; } = null!;

        public string Name { get; init; } = null!;
        
        public string Role { get; init; } = null!;
        
        public List<string> Permissions { get; init; } = null!;
    }
}
