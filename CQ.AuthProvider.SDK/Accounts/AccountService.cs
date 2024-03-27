using AutoMapper;
using CQ.AuthProvider.SDK.AppConfig;
using CQ.Utility;

namespace CQ.AuthProvider.SDK.Accounts
{
    internal sealed class AccountService : IAccountService
    {
        private readonly AuthProviderApi _authProviderApi;

        private readonly AuthProviderOptions _authProviderOptions;

        private readonly IMapper _mapper;

        public AccountService(
            AuthProviderApi authProviderApi,
            AuthProviderOptions authProviderOptions,
            IMapper mapper)
        {
            this._authProviderApi = authProviderApi;
            this._authProviderOptions = authProviderOptions;
            _mapper = mapper;
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
            var request = this._mapper.Map<CreateAccountPasswordRequest>(account);

            var successBody = await _authProviderApi
                .PostAsync<AccountResponse>(
                "accounts/credentials/for",
                request,
                new List<Header> { new("PrivateKey", this._authProviderOptions.PrivateKey) })
                .ConfigureAwait(false);

            return this._mapper.Map<Account>(successBody);
        }

        public async Task<AccountCreated> CreateAsync(CreateAccountPassword account)
        {
            var request = this._mapper.Map<CreateAccountPasswordRequest>(account);
            
            var successBody = await _authProviderApi
                .PostAsync<AccountLoggedResponse>(
                "accounts/credentials",
                request)
                .ConfigureAwait(false);

            return this._mapper.Map<AccountCreated>(successBody);
        }

        public async Task<Account> GetByTokenAsync(string token)
        {
            var successBody = await _authProviderApi.GetAsync<AccountResponse>(
                $"accounts/me",
                headers: new List<Header> { new("Authorization", token) })
                .ConfigureAwait(false);

            return this._mapper.Map<Account>(successBody);
        }
    }
}
