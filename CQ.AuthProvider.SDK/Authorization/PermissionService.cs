using CQ.AuthProvider.SDK.AppConfig;
using CQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK.Authorization
{
    internal sealed class PermissionService : IPermissionService
    {
        private readonly AuthProviderApi _authProviderApi;

        private readonly AuthProviderOptions _authProviderApiOptions;

        public PermissionService(
            AuthProviderApi authProviderApi,
            AuthProviderOptions authProviderApiOptions)
        {
            this._authProviderApi = authProviderApi;
            this._authProviderApiOptions = authProviderApiOptions;
        }

        public async Task AddBulkAsync(List<Permission> permissions)
        {
            await _authProviderApi.PostAsync(
              "permissions/bulk",
            new
            {
                Permissions = permissions,
            },
              headers: new List<Header> { new("PrivateKey", this._authProviderApiOptions.PrivateKey) })
              .ConfigureAwait(false);
        }
    }
}
