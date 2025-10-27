namespace CQ.AuthProvider.SDK.Abstractions.Sessions;
public interface ISessionService
{
    Task<SessionCreated> LoginAsync(CreateSessionPassword sessionPassword);

    Task<bool> IsTokenValidAsync(string token);
}
