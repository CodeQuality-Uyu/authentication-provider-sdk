using CQ.ApiElements;
using CQ.ApiElements.Filters.Extensions;
using CQ.AuthProvider.SDK.Accounts;
using CQ.AuthProvider.SDK.ClientSystems;
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

        public static ClientSystem GetClientSystemLogged(this ControllerBase controller)
        {
            return controller.GetItem<ClientSystem>(ContextItems.ClientSystemLogged);
        }
    }
}
