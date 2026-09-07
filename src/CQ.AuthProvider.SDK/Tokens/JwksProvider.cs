using CQ.AuthProvider.SDK.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CQ.AuthProvider.SDK.Tokens;

/// <summary>
/// Singleton: the key set is fetched once and reused by every request, which is
/// what keeps the local validation free of network calls.
/// </summary>
internal sealed class JwksProvider(
    IOptions<AuthProviderSection> authProviderOptions,
    IOptions<JwtSection> jwtOptions,
    ILogger<JwksProvider> logger)
    : IJwksProvider, IDisposable
{
    private const int MinSecondsBetweenForcedRefreshes = 30;

    private const int RetryAfterFailureInSeconds = 30;

    private readonly HttpClient _httpClient = new() { Timeout = TimeSpan.FromSeconds(10) };

    private readonly SemaphoreSlim _lock = new(1, 1);

    private readonly string _jwksUri = BuildJwksUri(authProviderOptions.Value, jwtOptions.Value);

    private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(jwtOptions.Value.KeysCacheInMinutes);

    private IReadOnlyList<SecurityKey> _keys = [];

    private DateTime _fetchedAt = DateTime.MinValue;

    private DateTime _expiresAt = DateTime.MinValue;

    public async Task<IReadOnlyList<SecurityKey>> GetAsync(bool forceRefresh = false)
    {
        if (!NeedsFetch(forceRefresh))
        {
            return _keys;
        }

        await _lock.WaitAsync().ConfigureAwait(false);
        try
        {
            // Whoever was holding the lock may have just fetched them.
            if (!NeedsFetch(forceRefresh))
            {
                return _keys;
            }

            var keys = await FetchOrDefaultAsync().ConfigureAwait(false);

            _fetchedAt = DateTime.UtcNow;

            if (keys == null)
            {
                // Hold on to what is cached (nothing, the first time) and stop
                // asking for a while: the endpoint being down must not turn
                // every authenticated request into a failed http call.
                _expiresAt = _fetchedAt.AddSeconds(RetryAfterFailureInSeconds);

                return _keys;
            }

            _keys = keys;
            _expiresAt = _fetchedAt.Add(_cacheDuration);

            return _keys;
        }
        finally
        {
            _lock.Release();
        }
    }

    private bool NeedsFetch(bool forceRefresh)
    {
        var now = DateTime.UtcNow;

        return forceRefresh
            ? now >= _fetchedAt.AddSeconds(MinSecondsBetweenForcedRefreshes)
            : now >= _expiresAt;
    }

    /// <summary>
    /// Null means "could not be read", which is different from "there are no
    /// keys": both send the caller back to the auth provider, but only the
    /// first one is worth retrying soon.
    /// </summary>
    private async Task<IReadOnlyList<SecurityKey>?> FetchOrDefaultAsync()
    {
        try
        {
            var response = await _httpClient
                .GetAsync(_jwksUri)
                .ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                logger.LogWarning(
                    "Jwks endpoint {JwksUri} answered {StatusCode}, tokens will be validated by the auth provider",
                    _jwksUri,
                    response.StatusCode);

                return null;
            }

            var json = await response.Content
                .ReadAsStringAsync()
                .ConfigureAwait(false);

            var keys = JsonWebKeySet.Create(json).GetSigningKeys();

            return keys.Count == 0
                ? null
                : [.. keys];
        }
        catch (Exception exception)
        {
            // Never let this break authentication: without keys the token is
            // resolved by the auth provider, exactly as it was before.
            logger.LogWarning(
                exception,
                "Could not read the jwks from {JwksUri}, tokens will be validated by the auth provider",
                _jwksUri);

            return null;
        }
    }

    private static string BuildJwksUri(
        AuthProviderSection authProvider,
        JwtSection jwt)
    {
        return string.IsNullOrWhiteSpace(jwt.JwksUri)
            ? $"{authProvider.Server?.TrimEnd('/')}/.well-known/jwks.json"
            : jwt.JwksUri;
    }

    public void Dispose()
    {
        _httpClient.Dispose();
        _lock.Dispose();
    }
}
