using CQ.ApiElements;
using CQ.ApiElements.Filters.Extensions;
using CQ.AuthProvider.SDK.Abstractions.Accounts;
using Microsoft.AspNetCore.Mvc;

namespace CQ.AuthProvider.SDK.ApiFilters.Extensions
{
    public static class ControllerBaseExtension
    {
        public static Account GetAccountLogged(this ControllerBase controller)
        {
            var accountLogged = controller.GetItem<Account>(ContextItems.AccountLogged);

            return accountLogged;
        }
    }
}
