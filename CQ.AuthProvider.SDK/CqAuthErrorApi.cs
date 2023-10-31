using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK
{
    public sealed record class CqAuthErrorApi
    {
        public string Code { get; set; }

        public CqAuthErrorCode AuthCode { get { return new CqAuthErrorCode(Code); } }

        public string Message { get; set; }
    }
}
