using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK.Exceptions
{
    public class CqAuthServiceException : Exception
    {
        public CqAuthErrorApi AuthError { get; }

        public CqAuthServiceException(CqAuthErrorApi authError) : base("Something went wrong when executing auth request") 
        {
            AuthError = authError;
        }
    }
}
