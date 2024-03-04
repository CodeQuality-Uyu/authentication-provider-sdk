using CQ.ApiElements.Filters;
using CQ.AuthProvider.SDK.Accounts;
using CQ.AuthProvider.SDK.Sessions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CQ.AuthProvider.SDK.ApiFilters
{
    public class CQAuthenticationAttribute : AuthenticationAsyncAttributeFilter
    {
        public CQAuthenticationAttribute() : base()
        {
        }

        public CQAuthenticationAttribute(string permission) : base(permission) 
        {
        }


        protected override async Task<bool> IsFormatOfTokenValidAsync(string token, AuthorizationFilterContext context)
        {
            var sessionService = base.GetService<ISessionService>(context);

            return await sessionService.IsTokenValidAsync(token).ConfigureAwait(false);
        }

        protected override async Task<object> GetAccountByTokenAsync(string token, AuthorizationFilterContext context)
        {
            var meService = base.GetService<IMeService>(context);

            var account = await meService.GetByTokenAsync(token).ConfigureAwait(false);

            return account;
        }
    }
}