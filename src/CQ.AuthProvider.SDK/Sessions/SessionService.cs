
using CQ.AuthProvider.SDK.Http;

namespace CQ.AuthProvider.SDK.Sessions;

internal sealed class SessionService(AuthProviderClient authProviderWebApi)
: ISessionService
{
    public async Task<SessionCreated> CreateAsync(CreateSessionArgs args)
    {
        var response = await authProviderWebApi
            .PostAsync<SessionCreated>("sessions/credentials", args, [])
            .ConfigureAwait(false);

        return response;
    }

    public async Task<SessionCreated> CreateWithGoogleAsync(CreateSessionGoogleArgs args)
    {
        var response = await authProviderWebApi
            .PostAsync<SessionCreated>("sessions/google", args, [])
            .ConfigureAwait(false);

        return response;
    }
}