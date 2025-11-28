using CQ.AuthProvider.SDK.Accounts;
using CQ.AuthProvider.SDK.Http;
using CQ.AuthProvider.SDK.Sessions;
using Newtonsoft.Json.Linq;

namespace CQ.AuthProvider.SDK.Apps;

internal sealed class AppService(
    AuthProviderConnectionApi authProviderWebApi)
    : IAppService
{
    public async Task<AppCreated> CreateAsync(CreateAppChildArgs args, AccountLogged accountLogged)
    {
        // Llamar este endpoint post app client para dar de alta a la aplicacion cliente en el auth provider
        var response = await authProviderWebApi
           .PostAsync<AppCreated>("apps/client", args, [])
           .ConfigureAwait(false);

        // Secuencial
        // Luego llamar el endpoint post accounts credentials for en el auth provider para crear las credenciales de la app cliente
        return response;

    }

    public Task<AppDetailedInfo> GetAsync(string token)
    {
        throw new NotImplementedException();
    }
}
