using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK
{
    public sealed record class AuthResult
    {

        public readonly string Id;

        public readonly string Email;

        public readonly IList<Roles> Roles;

        public AuthResult(string id, string email, IList<string> roles)
        {
            Id = id;
            Email = email;
            Roles = roles.Select(r => new Roles(r)).ToList();
        }
    }
}
