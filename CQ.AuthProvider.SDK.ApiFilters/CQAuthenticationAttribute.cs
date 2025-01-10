using CQ.ApiElements;
using CQ.ApiElements.Filters.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;

namespace CQ.AuthProvider.SDK.ApiFilters;


[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class CQAuthenticationAttribute : Attribute, IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var authorizationHeaderVaue = context.HttpContext.Request.Headers[HeaderNames.Authorization];

        var authenticationProviderApi = context.GetService<IAuthProviderConnection>();

        var accountLogged = await authenticationProviderApi
            .GetMeAsync(authorizationHeaderVaue)
            .ConfigureAwait(false);

        context.SetItem(ContextItem.AccountLogged, accountLogged);
    }
}