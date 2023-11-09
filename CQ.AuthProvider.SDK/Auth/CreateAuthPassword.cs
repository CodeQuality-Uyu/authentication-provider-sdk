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
        public string Email { get; init; }

        public string Password { get; init; }

        public string Role { get; init; }

        public CreateAuthPassword(string email, string password, string role)
        {
            Email = Guard.Encode(email.Trim());
            Password = Guard.Encode(password.Trim());
            Role = Guard.Encode(role.Trim());

            Guard.ThrowIsInputInvalidEmail(this.Email);

            Guard.ThrowIsInputInvalidPassword(this.Password);

            Guard.ThrowIsNullOrEmpty(this.Role, nameof(this.Role));
        }
    }
}
