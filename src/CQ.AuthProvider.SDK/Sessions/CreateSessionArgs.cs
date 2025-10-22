namespace CQ.AuthProvider.SDK.Sessions;

public readonly struct CreateSessionArgs()
{
    public required string Email { get; init; }

    public required string Password { get; init; }

    public Guid AppId { get; init; }
}