using CQ.ApiElements;
using CQ.ApiElements.Filters.Authorizations;
using CQ.AuthProvider.SDK.Accounts;
using CQ.AuthProvider.SDK.ClientSystems;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CQ.AuthProvider.SDK.ApiFilters
{
    public class CQAuthorizationAttribute : SecureAuthorizationAsyncAttributeFilter
    {
        public CQAuthorizationAttribute(string? permission=null) 
            : base(
                  new CQAuthenticationAttribute(),
                  permission)
        {
        }

        protected override Task<bool> HasRequestPermissionAsync(string headerValue, string permission, AuthorizationFilterContext context)
        {
            var accountLogged = context.HttpContext.Items[ContextItems.AccountLogged];
            var clientSystemLogged = context.HttpContext.Items[ContextItems.ClientSystemLogged];

            var permissionKey = new PermissionKey(permission);
            if (accountLogged != null)
            {
                var hasPermissionAccount = ((Account)accountLogged).HasPermission(permissionKey);

                return Task.FromResult(hasPermissionAccount);
            }

            var hasPermissionClient = ((ClientSystem)clientSystemLogged).HasPermission(permissionKey);

            return Task.FromResult(hasPermissionClient);
        }
    }
}