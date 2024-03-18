using CQ.Utility;

namespace CQ.AuthProvider.SDK.Accounts
{
    internal sealed class MeService : IMeService
    {
        private readonly AuthProviderApi _cqAuthApi;

        public MeService(AuthProviderApi cqAuthApi)
        {
            _cqAuthApi = cqAuthApi;
        }

        public async Task<Account> GetByTokenAsync(string token)
        {
            var successBody = await _cqAuthApi.GetAsync<AccountLoggedResponse>(
                "me",
                headers: new List<Header> { new("Authorization", token) })
                .ConfigureAwait(false);

            return new Account(
                successBody.Id,
                successBody.FullName,
                successBody.FirstName,
                successBody.LastName,
                successBody.Email,
                successBody.Roles,
                successBody.Permissions);
        }

        public async Task<bool> HasPermissionAsync(PermissionKey permission, string token)
        {
            var successBody = await _cqAuthApi.PostAsync<CheckPermissionResponse>(
               "me/check-permission",
               new
               {
                   Permission = permission.ToString(),
               },
               headers: new List<Header> { new("Authorization", token) })
               .ConfigureAwait(false);

            return successBody.HasPermission;
        }
    }
}
