using CQ.ApiElements;
using CQ.ApiElements.Filters.ExceptionFilter;
using CQ.ApiElements.Filters;
using CQ.ApiElements.Filters.Extensions;
using CQ.Utility;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using Microsoft.Net.Http.Headers;
using System.Security.Principal;

namespace CQ.AuthProvider.SDK.ApiFilters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class AuthorizationAttribute(string? _permission = null)
    : BaseAttribute, IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        try
        {
            if (Guard.IsNull(context.GetItemOrDefault(ContextItem.AccountLogged)))
            {
                var response = new ErrorResponse(HttpStatusCode.Unauthorized, "Unauthenticated", "Item not saved", string.Empty, "Missing item in context related to token in Auhtorization header");
                context.Result = BuildResponse(response);
                return;
            }

            var stringValues = context.HttpContext.Request.Headers[HeaderNames.Authorization];
            var (isAuthorized, permission) = await IsRequestAuthorizedAsync(stringValues, context).ConfigureAwait(continueOnCapturedContext: false);
            if (!isAuthorized)
            {
                var response2 = new ErrorResponse(HttpStatusCode.Forbidden, "Forbidden", "Insufficient permissions", string.Empty, $"You don't have the permission {permission} to access this request");
                context.Result = BuildResponse(response2);
            }
        }
        catch (Exception exception)
        {
            var response3 = BuildUnexpectedErrorResponse(exception);
            context.Result = BuildResponse(response3);
        }
    }

    private async Task<(bool isAuthorized, string permission)> IsRequestAuthorizedAsync(string headerValue, AuthorizationFilterContext context)
    {
        string permission = BuildPermission(headerValue, context);
        try
        {
            var hasPermission = await HasRequestPermissionAsync(headerValue, permission, context).ConfigureAwait(false);

            return (hasPermission, permission);
        }
        catch (Exception)
        {
            return (false, permission);
        }
    }

    private string BuildPermission(
        string headerValue,
        AuthorizationFilterContext context)
    {
        return _permission ?? $"{context.RouteData.Values["action"].ToString().ToLower()}-{context.RouteData.Values["controller"].ToString().ToLower()}";
    }

    private Task<bool> HasRequestPermissionAsync(string headerValue, string permission, AuthorizationFilterContext context)
    {
        return Task.FromResult(context.GetItem<IPrincipal>(ContextItem.AccountLogged).IsInRole(permission));
    }
}
