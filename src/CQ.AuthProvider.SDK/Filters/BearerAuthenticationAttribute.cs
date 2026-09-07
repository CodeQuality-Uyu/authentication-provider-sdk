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
using CQ.AuthProvider.SDK.Accounts;
using CQ.AuthProvider.SDK.Tokens;

namespace CQ.AuthProvider.SDK.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class BearerAuthenticationAttribute
    : BaseAttribute, IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        try
        {
            string authorizationHeaderVaue = context.HttpContext.Request.Headers[HeaderNames.Authorization]!;

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

            var accountLogged = await GetAccountLoggedAsync(context, authorizationHeaderVaue)
                .ConfigureAwait(false);

            context.SetItem(ContextItem.AccountLogged, accountLogged);

            await SetCustomAccountLoggedAsync(context).ConfigureAwait(false);
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

    /// <summary>
    /// A jwt carries the account in its claims and is validated against the
    /// public keys of the auth provider, so it resolves without leaving the
    /// process. An opaque token says nothing on its own and still needs GET /me.
    /// </summary>
    private static async Task<AccountLogged> GetAccountLoggedAsync(
        AuthorizationFilterContext context,
        string authorizationHeaderValue)
    {
        var accessTokenValidator = GetAccessTokenValidatorOrDefault(context);

        var accountLoggedFromToken = accessTokenValidator == null
            ? null
            : await accessTokenValidator
            .GetOrDefaultAsync(authorizationHeaderValue)
            .ConfigureAwait(false);

        if (accountLoggedFromToken != null)
        {
            return accountLoggedFromToken;
        }

        var meService = context.GetService<IMeService>();

        return await meService
            .GetAsync(authorizationHeaderValue)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// An app that wired the sdk by hand may not have it registered. Missing it
    /// only means every token is resolved by the auth provider, which is how
    /// this filter worked before jwt existed.
    /// </summary>
    private static IAccessTokenValidator? GetAccessTokenValidatorOrDefault(AuthorizationFilterContext context)
    {
        try
        {
            return context.GetService<IAccessTokenValidator>();
        }
        catch (Exception)
        {
            return null;
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

    protected virtual Task SetCustomAccountLoggedAsync(AuthorizationFilterContext context)
    {
        return Task.CompletedTask;
    }
}