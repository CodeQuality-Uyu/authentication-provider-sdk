using CQ.ApiElements;
using CQ.ApiElements.Filters;
using CQ.ApiElements.Filters.ExceptionFilter;
using CQ.ApiElements.Filters.Extensions;
using CQ.Utility;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;
using System.Net;
using System.Security.Principal;
using CQ.AuthProvider.SDK.Me;
using CQ.AuthProvider.SDK.Http;

namespace CQ.AuthProvider.SDK.ApiFilters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class BearerAuthenticationAttribute
    : BaseAttribute, IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        try
        {
            var authorizationHeaderVaue = context.HttpContext.Request.Headers[HeaderNames.Authorization];

            if (IsFakeAuthActiveAndSetIt(context) && Guard.IsNullOrEmpty(authorizationHeaderVaue))
            {
                return;
            }

            if (Guard.IsNullOrEmpty(authorizationHeaderVaue))
            {
                var response = new ErrorResponse(HttpStatusCode.Unauthorized, "Unauthenticated", "Missing Authorization header", string.Empty, "The endpoint is protected with authorization (needs to be sent Authorization header)");
                context.Result = BuildResponse(response);
                return;
            }

            var meService = context.GetService<IMeService>();

            var accountLogged = await meService
                .GetAsync(authorizationHeaderVaue)
                .ConfigureAwait(false);

            context.SetItem(ContextItem.AccountLogged, accountLogged);

            await SetCustomAccountLoggedAsync(context, accountLogged).ConfigureAwait(false);
        }
        catch (CqAuthException authError)
        {
            var errorResponse = new ErrorResponse(
                authError.StatusCode,
                authError.Code,
                authError.Message,
                string.Empty,
                authError.Description,
                authError);

            context.Result = BuildResponse(errorResponse);
        }
        catch (Exception exception)
        {
            var response = BuildUnexpectedErrorResponse(exception);
            context.Result = BuildResponse(response);
        }
    }

    private static bool IsFakeAuthActiveAndSetIt(AuthorizationFilterContext context)
    {
        try
        {
            var fakeAuthOrDefault = context.GetService<IPrincipal>();
            context.SetItem(ContextItem.AccountLogged, fakeAuthOrDefault);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    protected virtual Task SetCustomAccountLoggedAsync(
        AuthorizationFilterContext context,
        IPrincipal accountLogged)
    {
        return Task.CompletedTask;
    }
}