using AutoMapper;
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

        private readonly IMapper _mapper;

        public PermissionService(
            AuthProviderApi authProviderApi,
            AuthProviderOptions authProviderApiOptions,
            IMapper mapper)
        {
            this._authProviderApi = authProviderApi;
            this._authProviderApiOptions = authProviderApiOptions;
            this._mapper = mapper;
        }

        public async Task AddBulkAsync(List<Permission> permissions)
        {
            var request = this._mapper.Map<CreatePermissionBulkRequest>(permissions);

            await _authProviderApi.PostAsync(
              "permissions/bulk",
            request,
              headers: new List<Header> { new("PrivateKey", this._authProviderApiOptions.PrivateKey) })
              .ConfigureAwait(false);
        }
    }
}
