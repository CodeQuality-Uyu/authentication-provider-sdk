using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK
{
    public sealed record class CqAuthErrorCode
    {
        public static CqAuthErrorCode DuplicatedEmail = new("DuplicatedEmail");

        public static CqAuthErrorCode InvalidCredentials = new("InvalidCredentials");

        public static CqAuthErrorCode AuthDisabled= new("AuthDisabled");

        public string Value { get; }

        public CqAuthErrorCode(string value) 
        {
            Value = value;
        }
    }
}
