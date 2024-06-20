namespace CQ.AuthProvider.SDK.Abstractions.Accounts
{
    public interface IAccountService
    {
        Task<Account> CreateForAsync(CreateAccountPassword account);

        Task<AccountCreated> CreateAsync(CreateAccountPassword account);

        Task<Account> GetByTokenAsync(string token);
    }
}