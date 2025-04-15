using CQ.AuthProvider.SDK.Abstractions.Sessions;
using CQ.AuthProvider.SDK.Accounts;
using CQ.AuthProvider.SDK.Sessions;

namespace CQ.AuthProvider.SDK.AuthProviderConnections;
internal interface IAuthProviderConnection
{
    #region Health
    Task<bool> IsActiveAsync();
    #endregion

    #region Session
    Task<SessionResponse> LoginAsync(CreateSessionPassword credentials);

    Task<bool> IsTokenValidAsync(string token);
    #endregion

    #region Account
    Task<AccountCreatedResponse> CreateAccountForAsync(CreateAccountPasswordRequest request);

    Task<AccountLoggedResponse> CreateAccountAsync(CreateAccountPasswordRequest request);

    Task<AccountCreatedResponse> GetByTokenAsync(string token);
    #endregion
}
