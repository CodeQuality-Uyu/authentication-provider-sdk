namespace CQ.AuthProvider.SDK.Sessions;

public interface ISessionService
{
    Task<SessionCreated> CreateAsync(CreateSessionArgs args);
}