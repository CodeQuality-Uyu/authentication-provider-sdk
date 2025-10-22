
using CQ.AuthProvider.SDK.Http;

namespace CQ.AuthProvider.SDK.Sessions;

internal sealed class SessionService(AuthProviderConnectionApi authProviderWebApi)
: ISessionService
{
    public async Task<SessionCreated> CreateAsync(CreateSessionArgs args)
    {
        var response = await authProviderWebApi
            .PostAsync<SessionCreated>("sessions/credentials", args, [])
            .ConfigureAwait(false);

        return response;
    }
}