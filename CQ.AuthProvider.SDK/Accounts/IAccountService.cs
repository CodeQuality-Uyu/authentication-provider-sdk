namespace CQ.AuthProvider.SDK.Accounts
{
    public interface IAccountService
    {
        Task<Account> CreateForAsync(CreateAccountPassword account);
    }
}