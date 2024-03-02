using CQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK.Accounts
{
    public sealed record class CreateAccountPassword
    {
        public string Email { get; init; }

        public string FirstName { get; init; }

        public string LastName { get; init; }

        public string Password { get; init; }

        public string Role { get; init; }

        public CreateAccountPassword(
            string email,
            string firstName,
            string lastName,
            string password,
            string role)
        {
            Email = Guard.Encode(email.Trim());
            Password = Guard.Encode(password.Trim());
            Role = Guard.Encode(role.Trim());
            FirstName= Guard.Encode(firstName.Trim());
            LastName = Guard.Encode(lastName.Trim());

            Guard.ThrowIsInputInvalidEmail(this.Email);

            Guard.ThrowIsInputInvalidPassword(this.Password);

            Guard.ThrowIsNullOrEmpty(this.Role, nameof(this.Role));
            
            Guard.ThrowIsNullOrEmpty(this.FirstName, nameof(this.FirstName));

            Guard.ThrowIsNullOrEmpty(this.LastName, nameof(this.LastName));
        }
    }
}
