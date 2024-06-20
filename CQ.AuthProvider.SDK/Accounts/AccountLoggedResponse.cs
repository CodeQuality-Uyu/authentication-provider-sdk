namespace CQ.AuthProvider.SDK.Accounts;
internal record class AccountLoggedResponse : AccountCreatedResponse
{
    public string Token { get; init; } = null!;
}
