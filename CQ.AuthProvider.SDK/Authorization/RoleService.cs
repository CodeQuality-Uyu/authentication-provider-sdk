using CQ.AuthProvider.SDK.Accounts;
using CQ.AuthProvider.SDK.AppConfig;
using CQ.Utility;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK.Authorization
{
    internal sealed class RoleService : IRoleService
    {
        private readonly AuthProviderApi _authProviderApi;

        private readonly AuthProviderOptions _authProviderApiOptions;

        public RoleService(
            AuthProviderApi authProviderApi,
            AuthProviderOptions authProviderApiOptions)
        {
            this._authProviderApi = authProviderApi;
            this._authProviderApiOptions = authProviderApiOptions;
        }

        public async Task AddBulkAsync(List<Role> roles)
        {
            await _authProviderApi.PostAsync(
              "roles/bulk",
            new
            {
                Roles = roles,
            },
              headers: new List<Header> { new("PrivateKey", this._authProviderApiOptions.PrivateKey) })
              .ConfigureAwait(false);
        }
    }
}
