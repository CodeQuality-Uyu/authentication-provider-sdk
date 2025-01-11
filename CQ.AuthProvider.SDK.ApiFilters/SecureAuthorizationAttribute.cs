using CQ.ApiElements;
using CQ.ApiElements.Filters.ExceptionFilter;
using CQ.ApiElements.Filters;
using CQ.ApiElements.Filters.Extensions;
using CQ.AuthProvider.SDK.ApiFilters.Accounts;
using CQ.Utility;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace CQ.AuthProvider.SDK.ApiFilters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class SecureAuthorizationAttribute(string? Permission = null)
    : BaseAttribute, IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var accountLogged = context.GetItemOrDefault<AccountLogged>(ContextItem.AccountLogged);

        if (Guard.IsNull(accountLogged))
        {
            var response = new ErrorResponse(
                HttpStatusCode.Unauthorized,
                "Unauthenticated",
                "Item not saved",
                string.Empty,
                "Missing item in context related to token in Auhtorization header");
            context.Result = BuildResponse(response);

            return;
        }

        var hasPermission = await HasPermissionAsync(accountLogged, context).ConfigureAwait(false);

        if (!hasPermission)
        {
            var response = new ErrorResponse(
                HttpStatusCode.Forbidden,
                "Forbidden",
                "Insufficient permissions",
                string.Empty,
                $"You don't have the permission {Permission} to access this request");
            context.Result = BuildResponse(response);
        }
    }

    private Task<bool> HasPermissionAsync(AccountLogged accountLogged, AuthorizationFilterContext context)
    {
        Permission ??= $"{context.RouteData.Values["action"].ToString().ToLower()}-{context.RouteData.Values["controller"].ToString().ToLower()}";

        var hasPermission = accountLogged.HasPermission(Permission);

        return Task.FromResult(hasPermission);
    }
}

