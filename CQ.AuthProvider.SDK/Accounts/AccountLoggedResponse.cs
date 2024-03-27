namespace CQ.AuthProvider.SDK.Accounts
{
    public record class AccountLoggedResponse : AccountResponse
    {
        public string Token { get; init; } = null!;
    }
}
