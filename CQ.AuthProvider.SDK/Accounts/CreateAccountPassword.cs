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

        public RoleKey Role { get; init; }

        public CreateAccountPassword(
            string email,
            string firstName,
            string lastName,
            string password,
            RoleKey role)
        {
            Email = Guard.Encode(email, nameof(email));
            Password = Guard.Encode(password, nameof(password));
            Role = role;
            FirstName= Guard.Encode(firstName, nameof(firstName));
            LastName = Guard.Encode(lastName, nameof(lastName));

            Guard.ThrowIsInputInvalidEmail(this.Email);
            Guard.ThrowIsInputInvalidPassword(this.Password);
        }
    }
}
