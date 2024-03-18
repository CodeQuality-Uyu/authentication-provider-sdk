using CQ.AuthProvider.SDK.AppConfig;
using CQ.Utility;

namespace CQ.AuthProvider.SDK.Accounts
{
    internal sealed class AccountService : IAccountService
    {
        private readonly AuthProviderApi _authProviderApi;

        private readonly AuthProviderOptions _authProviderOptions;

        public AccountService(AuthProviderApi authProviderApi, AuthProviderOptions authProviderOptions)
        {
            this._authProviderApi = authProviderApi;
            this._authProviderOptions = authProviderOptions;
        }

        /// <summary>
        /// Creates an auth account in cq auth provider
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        /// <exception cref="DuplicatedEmailException"></exception>"
        /// <exception cref="RequestException<CqAuthErrorApi>"></exception>"
        public async Task<Account> CreateForAsync(CreateAccountPassword account)
        {
            var successBody = await _authProviderApi
                .PostAsync<AccountResponse>(
                "accounts/credentials/for",
                account,
                new List<Header> { new("PrivateKey", this._authProviderOptions.PrivateKey) })
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
    }
}
