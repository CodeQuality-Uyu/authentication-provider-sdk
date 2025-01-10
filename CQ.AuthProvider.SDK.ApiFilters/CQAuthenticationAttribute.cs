using Microsoft.AspNetCore.Mvc.Filters;

namespace CQ.AuthProvider.SDK.ApiFilters;


[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class CQAuthenticationAttribute : Attribute, IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var authorizationHeader = context.HttpContext.Request.Headers["Authorization"];

        var authenticationProviderApi = context.HttpContext.RequestServices.GetService<IAuthProviderConnection>();

        var accountLogged = await authenticationProviderApi
            .GetMeAsync(authorizationHeader)
            .ConfigureAwait(false);

        context.HttpContext.Items.Add("AccountLogged", accountLogged);
    }
}