using CQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK
{
    public record CreatePasswordAuth
    {
        public string Email { get; }

        public string Password { get; }

        public CreatePasswordAuth(string? email, string? password)
        {
            Email = Guard.Encode(email?.Trim() ?? string.Empty);
            Password = Guard.Encode(password?.Trim() ?? string.Empty);

            Guard.ThrowIsInputInvalidEmail(Email);

            Guard.ThrowIsInputInvalidPassword(Password);
        }
    }
}
