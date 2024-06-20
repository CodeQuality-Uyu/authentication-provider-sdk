using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK.AuthProviderConnections.Api
{
    public sealed record class CqAuthErrorCode
    {
        public static readonly CqAuthErrorCode Unauthenticated = new("Unauthenticated");

        public static readonly CqAuthErrorCode InvalidTokenFormat = new("InvalidTokenFormat");

        public static readonly CqAuthErrorCode AccessDenied = new("AccessDenied");

        public static readonly CqAuthErrorCode RequestInvalid = new("RequestInvalid");

        public static readonly CqAuthErrorCode ResourceDuplicated = new("ResourceDuplicated");

        public static readonly CqAuthErrorCode InvalidOperation = new("InvalidOperation");

        public static readonly CqAuthErrorCode DuplicatedEmail = new("DuplicatedEmail");

        public static readonly CqAuthErrorCode InvalidRole = new("InvalidRole");

        public static readonly CqAuthErrorCode InvalidCredentials = new("InvalidCredentials");

        public static readonly CqAuthErrorCode InvalidSession = new("InvalidSession");

        public static readonly CqAuthErrorCode AuthDisabled = new("AuthDisabled");

        private readonly string Value;

        public CqAuthErrorCode(string value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
