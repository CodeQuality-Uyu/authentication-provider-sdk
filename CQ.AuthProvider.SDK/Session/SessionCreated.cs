using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK
{
    public sealed record class SessionCreated
    {
        public readonly string AuthId;

        public readonly string Email;

        public readonly string Token;

        public readonly IList<Roles> Roles;

        public SessionCreated(string authId, string email, string token, IList<string> roles)
        {
            AuthId = authId;
            Email = email;
            Token = token;
            Roles = roles.Select(s => new Roles(s)).ToList();
        }
    }
}
