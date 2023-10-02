using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK.Exceptions
{
    internal class DuplicatedEmailException : Exception
    {
        public string Email { get; set; }

        public DuplicatedEmailException(string email):base($"The email {email} is duplicated") { Email = email; }
    }
}
