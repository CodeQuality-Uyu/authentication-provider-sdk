namespace CQ.AuthProvider.SDK.Abstractions.Accounts;
public sealed record class AccountCreated : Account
{
    public string Token { get; init; } = null!;
}
