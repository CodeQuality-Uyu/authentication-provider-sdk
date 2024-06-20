namespace CQ.AuthProvider.SDK.Accounts;
internal sealed class CreateAccountPasswordRequest
{
    public string Email { get; init; } = null!;

    public string FirstName { get; init; } = null!;

    public string LastName { get; init; } = null!;

    public string Password { get; init; } = null!;

    public string? Role { get; init; } = null!;
}
