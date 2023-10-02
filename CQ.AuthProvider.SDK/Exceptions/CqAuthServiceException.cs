using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK.Exceptions
{
    public class CqAuthServiceException : Exception
    {
        public CqAuthServiceException() : base("Something went wrong when executing request http") { }
    }
}
