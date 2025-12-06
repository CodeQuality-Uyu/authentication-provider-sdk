namespace CQ.AuthProvider.SDK.Accounts;

public interface IAccountService
{
    Task<AccountCreated> CreateAsync(CreateAccountPasswordArgs args);
    
    Task<AccountCreated> CreateForAsync(CreateAccountForArgs args, AccountLogged accountLogged);
}