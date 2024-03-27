using CQ.ApiElements.Filters.Authentications;
using CQ.AuthProvider.SDK.Accounts;
using CQ.AuthProvider.SDK.ClientSystems;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CQ.AuthProvider.SDK.ApiFilters
{
    public class CQAuthenticationAttribute : SecureAuthenticationAsyncAttributeFilter
    {
        protected override async Task<object> GetRequestByHeaderAsync(string header, string headerValue, AuthorizationFilterContext context)
        {
            if (header == "Authorization")
            {
                var meService = base.GetService<IAccountService>(context);

                var account = await meService.GetByTokenAsync(headerValue).ConfigureAwait(false);

                return account;
            }

            var clientSystemService = base.GetService<IClientSystemsService>(context);

            var clientSystem = await clientSystemService.GetByPrivateKeyAsync(headerValue).ConfigureAwait(false);

            return clientSystem;
        }
    }
}