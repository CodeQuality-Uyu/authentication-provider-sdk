using CQ.AuthProvider.SDK.Abstractions.Sessions;
using CQ.AuthProvider.SDK.Accounts;
using CQ.AuthProvider.SDK.AppConfig;
using CQ.AuthProvider.SDK.HealthChecks;
using CQ.AuthProvider.SDK.Sessions;
using CQ.Utility;

namespace CQ.AuthProvider.SDK.AuthProviderConnections.Api;
internal sealed class AuthProviderConnectionApi(AuthProviderSection _section) :
    ConcreteHttpClient<CqAuthErrorApi>(_section.Server),
    IAuthProviderConnection
{
    protected override Exception? ProcessError(CqAuthErrorApi error)
    {
        return new CqAuthException(error.AuthCode, error.Message);
    }

    public async Task<bool> IsActiveAsync()
    {
        var response = await base
            .GetAsync<HealthResponse>("health")
            .ConfigureAwait(false);

        return response.IsActive;
    }

    public async Task<SessionResponse> LoginAsync(CreateSessionPassword credentials)
    {
        var response = await base
            .PostAsync<SessionResponse>(
            "sessions/credentials",
            credentials)
            .ConfigureAwait(false);

        return response;
    }

    public async Task<bool> IsTokenValidAsync(string token)
    {
        var response = await base
                .GetAsync<TokenValidationResponse>(
                $"sessions/{token}/validate",
                [new("PrivateKey", _section.PrivateKey)])
                .ConfigureAwait(false);

        return response.IsValid;
    }

    public async Task<AccountCreatedResponse> CreateAccountForAsync(CreateAccountPasswordRequest request)
    {
        var response = await base
                .PostAsync<AccountCreatedResponse>(
                "accounts/credentials/for",
                request,
                [new("PrivateKey", _section.PrivateKey)])
                .ConfigureAwait(false);

        return response;
    }

    public async Task<AccountLoggedResponse> CreateAccountAsync(CreateAccountPasswordRequest request)
    {
        var response = await base
                .PostAsync<AccountLoggedResponse>(
                "accounts/credentials",
                request)
                .ConfigureAwait(false);

        return response;
    }

    public async Task<AccountCreatedResponse> GetByTokenAsync(string token)
    {
        var response = await base
            .GetAsync<AccountCreatedResponse>(
                $"accounts/me",
                [new("Authorization", token)])
            .ConfigureAwait(false);

        return response;
    }
}
