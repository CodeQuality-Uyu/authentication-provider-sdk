using CQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK
{
    public sealed record class CreateAuthPassword
    {
        public string Email { get; }

        public string Password { get; }

        public CreateAuthPassword(string email, string password)
        {
            Email = Guard.Encode(email.Trim());
            Password = Guard.Encode(password.Trim());

            Guard.ThrowIsInputInvalidEmail(Email);

            Guard.ThrowIsInputInvalidPassword(Password);
        }
    }
}
