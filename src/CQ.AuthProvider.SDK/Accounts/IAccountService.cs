using CQ.AuthProvider.SDK.Sessions;

namespace CQ.AuthProvider.SDK.Accounts;

public interface IAccountService
{
    Task<SessionCreated> CreateAsync(CreateAccountPasswordArgs args);
    
    Task<AccountCreated> CreateForAsync(CreateAccountForArgs args, AccountLogged accountLogged);
}