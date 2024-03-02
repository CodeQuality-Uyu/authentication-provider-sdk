﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK.Accounts
{
    public sealed record class Account
    {
        public string Id { get; init; } = null!;

        public string Name { get; init; } = null!;

        public string Email { get; init; } = null!;

        public string Token { get; init; } = null!;

        public List<string> Roles { get; init; } = null!;
        
        public List<string> Permissions { get; init; } = null!;
    }
}
