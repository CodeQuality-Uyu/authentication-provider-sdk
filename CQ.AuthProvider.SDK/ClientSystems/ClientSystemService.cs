using AutoMapper;
using CQ.AuthProvider.SDK.AppConfig;
using CQ.Utility;

namespace CQ.AuthProvider.SDK.ClientSystems
{
    internal sealed class ClientSystemService : IClientSystemsService
    {

        private readonly AuthProviderApi _cqAuthApi;

        private readonly IMapper _mapper;


        public ClientSystemService(
            AuthProviderApi cqAuthApi,
            IMapper mapper)
        {
            _cqAuthApi = cqAuthApi;
            _mapper = mapper;
        }

        public async Task<ClientSystem> GetByPrivateKeyAsync(string privateKey)
        {
            var successBody = await _cqAuthApi.GetAsync<ClientSystemResponse>(
                $"client-systems/me",
                headers: new List<Header> { new("PrivateKey", privateKey) })
                .ConfigureAwait(false);

            return this._mapper.Map<ClientSystem>(successBody);
        }
    }
}
