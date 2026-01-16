using CQ.AuthProvider.SDK.Accounts;

namespace CQ.AuthProvider.SDK.Apps;

public interface IAppService
{
    Task<AppCreated> CreateClientAsync(CreateAppChildArgs args, AccountLogged accountLogged);

    Task<AppDetailedInfo> GetByIdAsync(Guid id, AccountLogged accountLogged);

    Task<AppDetailedInfo> GetByIdAsync(Guid id, string subscriptionKey);
}
