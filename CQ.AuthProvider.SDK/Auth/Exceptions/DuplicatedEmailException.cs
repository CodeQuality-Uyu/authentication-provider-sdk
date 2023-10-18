using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK.Exceptions
{
    public class DuplicatedEmailException : Exception
    {
        public string Email { get; set; }

        public DuplicatedEmailException(string email) { Email = email; }
    }
}
