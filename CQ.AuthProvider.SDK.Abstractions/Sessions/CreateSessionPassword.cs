using CQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK.Abstractions.Sessions
{
    public sealed record class CreateSessionPassword
    {
        public string Email { get; init; }

        public string Password { get; init; }

        public CreateSessionPassword(string email, string password)
        {
            Email = Guard.Encode(email, nameof(email));
            Password = Guard.Encode(password, nameof(password));
        }
    }
}
