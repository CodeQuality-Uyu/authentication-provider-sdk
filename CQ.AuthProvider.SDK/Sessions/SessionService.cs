using AutoMapper;
using CQ.AuthProvider.SDK.Abstractions.Sessions;
using CQ.AuthProvider.SDK.AuthProviderConnections;

namespace CQ.AuthProvider.SDK.Sessions;
internal class SessionService(
    IMapper _mapper,
    IAuthProviderConnection _authProviderConnection) :
    ISessionService
{
    public async Task<SessionCreated> LoginAsync(CreateSessionPassword sessionPassword)
    {
        var response = await _authProviderConnection
            .LoginAsync(sessionPassword)
            .ConfigureAwait(false);

        return _mapper.Map<SessionCreated>(response);
    }

    public async Task<bool> IsTokenValidAsync(string token)
    {
        var response = await _authProviderConnection
            .IsTokenValidAsync(token)
            .ConfigureAwait(false);

        return response;
    }
}
