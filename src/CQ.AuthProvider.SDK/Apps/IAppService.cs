

using CQ.AuthProvider.SDK.Accounts;

namespace CQ.AuthProvider.SDK.Apps;

public interface IAppService
{
    Task<AppDetailedInfo> GetAsync(Guid id, AccountLogged accountLogged);
    Task<AppCreated> CreateAsync(CreateAppChildArgs args, AccountLogged accountLogged);
}
