﻿using CQ.ApiElements;
using CQ.ApiElements.Filters.Authorizations;
using CQ.AuthProvider.SDK.Abstractions.Accounts;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CQ.AuthProvider.SDK.ApiFilters;
public class CQAuthorizationAttribute(string? permission = null) :
    SecureAuthorizationAttribute(
        new CQAuthenticationAttribute(),
        permission)
{
    protected override Task<bool> HasRequestPermissionAsync(
        string headerValue,
        string permission,
        AuthorizationFilterContext context)
    {
        var accountLogged = base.GetItem<Account>(context, ContextItems.AccountLogged);

        var permissionKey = new PermissionKey(permission);
        var hasPermissionAccount = accountLogged.HasPermission(permissionKey);

        return Task.FromResult(hasPermissionAccount);
    }
}