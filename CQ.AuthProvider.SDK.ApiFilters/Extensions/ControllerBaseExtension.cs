using CQ.ApiElements;
using CQ.ApiElements.Filters.Extensions;
using CQ.AuthProvider.SDK.ApiFilters.Accounts;
using Microsoft.AspNetCore.Mvc;

namespace CQ.AuthProvider.SDK.ApiFilters.Extensions
{
    public static class ControllerBaseExtension
    {
        public static AccountLogged GetAccountLogged(this ControllerBase controller)
        {
            var accountLogged = controller.GetItem<AccountLogged>(ContextItem.AccountLogged);

            return accountLogged;
        }
    }
}
