using CQ.Utility;

namespace CQ.AuthProvider.SDK.Accounts
{
    public sealed class MeService : IMeService
    {
        private readonly AuthProviderApi _cqAuthApi;

        public MeService(AuthProviderApi cqAuthApi)
        {
            _cqAuthApi = cqAuthApi;
        }

        public async Task<AccountResult> GetByTokenAsync(string token)
        {
            var successBody = await _cqAuthApi.GetAsync<AccountLogged>(
                "me",
                headers: new List<Header> { new("Authorization", token) })
                .ConfigureAwait(false);

            return new AccountResult(
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
            var successBody = await _cqAuthApi.PostAsync<CheckPermissionResult>(
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
