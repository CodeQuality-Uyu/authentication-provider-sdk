using CQ.ApiElements;
using CQ.ApiElements.Filters.Extensions;
using CQ.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;
using System.Net;

namespace CQ.AuthProvider.SDK.ApiFilters;


[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class BearerAuthenticationAuthProviderAttribute
    : Attribute, IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        try
        {
            var authorizationHeaderVaue = context.HttpContext.Request.Headers[HeaderNames.Authorization];

            var authenticationProviderApi = context.GetService<IAuthProviderConnection>();

            var accountLogged = await authenticationProviderApi
                .GetMeAsync(authorizationHeaderVaue)
                .ConfigureAwait(false);

            context.SetItem(ContextItem.AccountLogged, accountLogged);
        }
        catch (RequestException<object> ex)
        {
            context.Result = new ObjectResult(ex.ErrorBody)
            {
                StatusCode = (int)HttpStatusCode.Unauthorized
            };
        }
    }
}