using AutoMapper;
using CQ.AuthProvider.SDK.AppConfig;
using CQ.Utility;

namespace CQ.AuthProvider.SDK.Sessions
{
    internal class SessionService : ISessionService
    {
        private readonly AuthProviderApi _cqAuthApi;

        private readonly IMapper _mapper;

        private readonly AuthProviderOptions _authProviderOptions;

        public SessionService(
            AuthProviderApi cqAuthApi,
            IMapper mapper,
            AuthProviderOptions authProviderOptinos)
        {
            _cqAuthApi = cqAuthApi;
            _mapper = mapper;
            _authProviderOptions = authProviderOptinos;
        }

        /// <summary>
        /// Create a session for the user with those credentials
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <exception cref="RequestException<CqAuthErrorApi>"></exception>"
        public async Task<SessionCreated> LoginAsync(CreateSessionPassword sessionPassword)
        {
            var successBody = await _cqAuthApi.PostAsync<SessionResponse>(
                "sessions/credentials",
                sessionPassword)
                .ConfigureAwait(false);

            return this._mapper.Map<SessionCreated>(successBody);
        }

        public async Task<bool> IsTokenValidAsync(string token)
        {
            var successBody = await this._cqAuthApi
                .GetAsync<TokenValidationResponse>(
                $"sessions/{token}/validate",
                new List<Header> { new Header("PrivateKey", _authProviderOptions.PrivateKey) })
                .ConfigureAwait(false);

            return successBody.IsValid;
        }
    }
}
