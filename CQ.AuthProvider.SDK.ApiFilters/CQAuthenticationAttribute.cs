using CQ.ApiElements.Filters.Authentications;
using CQ.AuthProvider.SDK.Abstractions.Accounts;
using CQ.AuthProvider.SDK.AppConfig;
using CQ.Exceptions;
using CQ.Utility;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CQ.AuthProvider.SDK.ApiFilters;
public class CQAuthenticationAttribute : SecureAuthenticationAttribute
{
    public override async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var authenticationSection = base.GetService<AuthProviderSection>(context);

        if (authenticationSection.Fake.IsActive)
        {
            context.HttpContext.Request.Headers["Authorization"] = "fake-token";
        }

        await base
            .OnAuthorizationAsync(context)
            .ConfigureAwait(false);
    }

    protected override async Task<object> GetRequestByHeaderAsync(
        string header,
        string headerValue,
        AuthorizationFilterContext context)
    {
        if (Guard.IsNot(header, "Authorization"))
        {
            throw new InvalidHeaderException(header, headerValue);
        }

        var meService = base.GetService<IAccountService>(context);

        var account = await meService
            .GetByTokenAsync(headerValue)
            .ConfigureAwait(false);

        return account;
    }
}