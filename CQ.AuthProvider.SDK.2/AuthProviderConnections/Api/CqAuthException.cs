using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK.AuthProviderConnections.Api
{
    public sealed class CqAuthException : Exception
    {
        public readonly CqAuthErrorCode ErrorCode;

        public CqAuthException(CqAuthErrorCode errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
