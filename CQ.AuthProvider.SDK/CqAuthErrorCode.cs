using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK
{
    public sealed record class CqAuthErrorCode
    {
        public static readonly CqAuthErrorCode DuplicatedEmail = new("DuplicatedEmail");

        public static readonly CqAuthErrorCode InvalidCredentials = new("InvalidCredentials");

        public static readonly CqAuthErrorCode AuthDisabled = new("AuthDisabled");

        public readonly string Value;

        public CqAuthErrorCode(string value) 
        {
            Value = value;
        }
    }
}
