using CQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK.Sessions
{
    public sealed record class CreateSessionPassword
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public CreateSessionPassword(string email, string password)
        {
            Email = Guard.Encode(email.Trim());
            Password = Guard.Encode(password.Trim());

            Guard.ThrowIsNullOrEmpty(email,"email");
            Guard.ThrowIsNullOrEmpty(password, "password");
        }
    }
}
