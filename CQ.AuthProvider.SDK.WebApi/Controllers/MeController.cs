using CQ.AuthProvider.SDK.ApiFilters;
using CQ.AuthProvider.SDK.ApiFilters.Accounts;
using CQ.AuthProvider.SDK.ApiFilters.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CQ.AuthProvider.SDK.WebApi.Controllers
{
    [ApiController]
    [Route("me")]
    [BearerAuthenticationAuthProvider]
    public class MeController : ControllerBase
    {
        [HttpGet]
        public AccountLogged Get([FromHeader] string authorization)
        {
            var accountLogged = this.GetAccountLogged();

            return accountLogged;
        }

        [HttpPost("check-permission")]
        [Authorization]
        public object CheckPermission([FromHeader] string authorization, [FromBody] CheckPermissionRequest request)
        {
            var accountLogged = this.GetAccountLogged();

            return new
            {
                hasPermission = accountLogged.IsInRole(request.Permission)
            };
        }
    }

    public sealed record class CheckPermissionRequest(string Permission);
}
