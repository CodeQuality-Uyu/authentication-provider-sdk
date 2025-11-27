

using CQ.AuthProvider.SDK.Accounts;

namespace CQ.AuthProvider.SDK.Apps;

public interface IAppService
{
    Task<AppDetailedInfo> GetAsync(string token);

    Task CreateAsync(CreateAppChildArgs args, AccountLogged accountLogged);
}
