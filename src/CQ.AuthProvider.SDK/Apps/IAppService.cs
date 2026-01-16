

using CQ.AuthProvider.SDK.Accounts;

namespace CQ.AuthProvider.SDK.Apps;

public interface IAppService
{
    Task<AppCreated> CreateClientAsync(CreateAppChildArgs args, AccountLogged accountLogged);

    Task<AppDetailedInfo> GetAsync(Guid id, AccountLogged accountLogged);

    Task<AppDetailedInfo> GetAsync(Guid id, string subscriptionKey);
}
