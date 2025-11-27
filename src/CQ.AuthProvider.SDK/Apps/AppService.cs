using CQ.AuthProvider.SDK.Accounts;

namespace CQ.AuthProvider.SDK.Apps;

public class AppService
    : IAppService
{
    public Task CreateAsync(CreateAppChildArgs args, AccountLogged accountLogged)
    {
        // Llamar este endpoint post app client para dar de alta a la aplicacion cliente en el auth provider
        // Secuencial
        // Luego llamar el endpoint post accounts credentials for en el auth provider para crear las credenciales de la app cliente
        throw new NotImplementedException();

    }

    public Task<AppDetailedInfo> GetAsync(string token)
    {
        throw new NotImplementedException();
    }
}
