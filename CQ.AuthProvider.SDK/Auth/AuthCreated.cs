using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK
{
    public sealed record class AuthCreated
    {
        public readonly string Id;

        public readonly string Email;

        public readonly string Token;

        public readonly IList<Roles> Roles;

        public AuthCreated(string id, string email, string token, IList<string> roles)
        {
            Id = id;
            Email = email;
            Token = token;
            Roles = roles.Select(r => new Roles(r)).ToList();
        }
    }
}
