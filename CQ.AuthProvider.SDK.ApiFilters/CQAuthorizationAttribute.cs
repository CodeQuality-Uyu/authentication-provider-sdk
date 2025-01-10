using CQ.ApiElements;
using CQ.ApiElements.Filters.Extensions;
using CQ.AuthProvider.SDK.ApiFilters.Accounts;
using CQ.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CQ.AuthProvider.SDK.ApiFilters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class CQAuthorizationAttribute(string? Permission = null)
    : Attribute, IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var accountLogged = context.GetItemOrDefault<AccountLogged>(ContextItem.AccountLogged);

        if (Guard.IsNull(accountLogged))
        {
            context.Result = new ObjectResult()
            {
                StatusCode = 1
            };
            return;
        }

        var hasPermission = await HasPermissionAsync(accountLogged, context).ConfigureAwait(false);

        if (!hasPermission)
        {
            context.Result = new ObjectResult()
            {
                StatusCode = 1
            };
            return;
        }
    }

    private Task<bool> HasPermissionAsync(AccountLogged accountLogged, AuthorizationFilterContext context)
    {
        var permission = Permission ?? $"{context.RouteData.Values["action"].ToString().ToLower()}-{context.RouteData.Values["controller"].ToString().ToLower()}";
        
        var hasPermission = accountLogged.HasPermission(permission);

        return Task.FromResult(hasPermission);
    }
}

