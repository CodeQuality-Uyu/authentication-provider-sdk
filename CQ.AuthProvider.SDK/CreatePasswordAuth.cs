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

            Guard.ThrowIsNullOrEmpty(Email, "email");
            Guard.ThrowEmailFormat(Email);

            Guard.ThrowIsNullOrEmpty(Password, "password");
            Guard.ThrowMinimumLength(Password, 8, "password");
            Guard.ThrowPasswordFormat(Password);
        }
    }
}
