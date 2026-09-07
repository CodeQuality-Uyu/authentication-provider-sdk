namespace CQ.AuthProvider.SDK.Sessions;

public readonly struct CreateSessionGoogleArgs()
{
    public required string IdToken { get; init; }

    public Guid AppId { get; init; }
}
