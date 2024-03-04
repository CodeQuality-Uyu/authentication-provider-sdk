using CQ.ApiElements;
using CQ.ApiElements.Filters.Extensions;
using CQ.AuthProvider.SDK.Accounts;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CQ.AuthProvider.SDK.ApiFilters
{
    public class CQAuthorizationAttribute : CQAuthenticationAttribute
    {
        public CQAuthorizationAttribute() 
            : base()
        {
        }

        public CQAuthorizationAttribute(string permission) 
            : base(permission)
        {
        }

        protected override Task<bool> HasUserPermissionAsync(string token, string permission, AuthorizationFilterContext context)
        {
            var account = context.HttpContext.GetItem<AccountResult>(ContextItems.AccountLogged);

            var hasPermission = account.Permissions.Contains(new PermissionKey(permission));

            return Task.FromResult(hasPermission);
        }
    }
}