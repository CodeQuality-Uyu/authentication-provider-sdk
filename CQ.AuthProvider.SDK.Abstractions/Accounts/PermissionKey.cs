﻿using CQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK.Abstractions.Accounts
{
    public record class PermissionKey
    {
        private readonly string Value;

        public PermissionKey(string value)
        {
            Value = Guard.Encode(value, nameof(value));
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
