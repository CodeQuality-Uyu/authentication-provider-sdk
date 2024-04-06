using AutoMapper;
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

        private readonly IMapper _mapper;

        public RoleService(
            AuthProviderApi authProviderApi,
            AuthProviderOptions authProviderApiOptions,
            IMapper mapper)
        {
            this._authProviderApi = authProviderApi;
            this._authProviderApiOptions = authProviderApiOptions;
            this._mapper = mapper;
        }

        public async Task AddBulkAsync(List<Role> roles)
        {
            var request = this._mapper.Map<CreateRoleBulkRequest>(roles);

            await _authProviderApi.PostAsync(
              "roles/bulk",
            request,
              headers: new List<Header> { new("PrivateKey", this._authProviderApiOptions.PrivateKey) })
              .ConfigureAwait(false);
        }

        public async Task AddBulkAsync(params Role[] roles)
        {
            await this.AddBulkAsync(roles.ToList()).ConfigureAwait(false);
        }
    }
}
