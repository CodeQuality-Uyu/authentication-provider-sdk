namespace CQ.AuthProvider.SDK.Accounts
{
    public sealed class AccountService : IAccountService
    {
        private readonly AuthProviderApi _cqAuthApi;

        public AccountService(AuthProviderApi cqAuthApi)
        {
            _cqAuthApi = cqAuthApi;
        }

        /// <summary>
        /// Creats an auth account in cq auth provider
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        /// <exception cref="DuplicatedEmailException"></exception>"
        /// <exception cref="RequestException<CqAuthErrorApi>"></exception>"
        public async Task<AccountCreated> CreateAsync(CreateAccountPassword account)
        {
            var successBody = await _cqAuthApi.PostAsync<Account>("accounts/credentials", account).ConfigureAwait(false);

            return new AccountCreated(
                successBody.Id,
                successBody.FullName,
                successBody.FirstName,
                successBody.LastName,
                successBody.Email,
                successBody.Token,
                successBody.Roles,
                successBody.Permissions);
        }
    }
}
