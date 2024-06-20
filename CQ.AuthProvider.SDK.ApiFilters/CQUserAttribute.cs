using CQ.ApiElements;
using CQ.ApiElements.Filters.Authentications;
using CQ.AuthProvider.SDK.Abstractions.Accounts;
using CQ.AuthProvider.SDK.Abstractions.Users;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CQ.AuthProvider.SDK.ApiFilters;
public class CQUserAttribute : SecureUserAttribute
{
    protected override async Task<object> GetUserLoggedAsync(AuthorizationFilterContext context)
    {
        var accountLogged = base.GetItem<Account>(context, ContextItems.AccountLogged);

        var userService = base.GetService<IUserService>(context);

        var userLogged = await userService
            .GetUserOfAccountAsync(accountLogged.Id)
            .ConfigureAwait(false);

        return userLogged;
    }
}
