namespace CQ.AuthProvider.SDK.Accounts;

public readonly struct CreateAccountForArgs()
{
    public required string Email { get; init; }

    public required string Password { get; init; }

    public required string FirstName { get; init; }

    public required string LastName { get; init; }

    public required string Locale { get; init; }

    public required string TimeZone { get; init; }

    public Guid ProfilePictureId { get; init; }

    public required IList<Guid> AppIds { get; init; }

    public required IList<Guid> RoleIds { get; init; }
}

