namespace CQ.AuthProvider.SDK.Accounts;

public readonly struct CreateAccountPasswordArgs()
{
    public string? ProfilePictureKey { get; init; }

    public required string Email { get; init; }

    public required string FirstName { get; init; }

    public required string LastName { get; init; }

    public required string Password { get; init; }

    public required string Locale { get; init; }

    public required string TimeZone { get; init; }

    public bool IsPasswordHashed { get; init; }

    public required Guid AppId { get; init; }

    public Guid? RoleId { get; init; }
}
