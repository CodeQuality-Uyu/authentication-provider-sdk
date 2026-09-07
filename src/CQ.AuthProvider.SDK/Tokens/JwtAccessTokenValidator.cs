using System.Net;
using System.Text.Json;
using CQ.AuthProvider.SDK.Accounts;
using CQ.AuthProvider.SDK.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace CQ.AuthProvider.SDK.Tokens;

/// <summary>
/// Validates the self contained access tokens issued by the auth provider:
/// signature against the published keys, issuer, audience and expiration, and
/// rebuilds the <see cref="AccountLogged"/> from the claims. No call to the
/// provider, which is the whole point of the format.
/// </summary>
/// <remarks>
/// Being self contained, an access token stays valid until it expires even
/// after logout. That window is the provider's
/// "AccessTokenExpirationInMinutes"; an app that cannot live with it has to
/// keep resolving the token against the provider (<c>Authentication:Jwt:IsActive</c>
/// set to false).
/// </remarks>
internal sealed class JwtAccessTokenValidator(
    IJwksProvider keyProvider,
    IOptions<JwtSection> options)
    : IAccessTokenValidator
{
    private const string BearerScheme = "Bearer ";

    private static readonly JsonWebTokenHandler _handler = new();

    private readonly JwtSection _jwt = options.Value;

    public async Task<AccountLogged?> GetOrDefaultAsync(string authorizationHeaderValue)
    {
        if (!_jwt.IsActive || string.IsNullOrWhiteSpace(authorizationHeaderValue))
        {
            return null;
        }

        var token = RemoveScheme(authorizationHeaderValue);

        // A JWT has three base64url segments, every other token handed out by
        // the provider is a bare guid, so the formats never overlap.
        if (!_handler.CanReadToken(token))
        {
            return null;
        }

        var keys = await keyProvider
            .GetAsync()
            .ConfigureAwait(false);

        if (keys.Count == 0)
        {
            // Nothing to validate against. Rejecting would be a lie: the token
            // may well be fine, and the provider can still say so.
            return null;
        }

        var result = await ValidateAsync(token, keys).ConfigureAwait(false);

        if (!result.IsValid || result.SecurityToken is not JsonWebToken jwt)
        {
            throw BuildError(result.Exception);
        }

        var payload = ReadPayload(jwt);

        return payload == null
            ? throw BuildError(null)
            : BuildAccountLogged(payload, token);
    }

    private async Task<TokenValidationResult> ValidateAsync(
        string token,
        IReadOnlyList<SecurityKey> keys)
    {
        var result = await _handler
            .ValidateTokenAsync(token, BuildValidationParameters(keys))
            .ConfigureAwait(false);

        if (result.IsValid || result.Exception is not SecurityTokenSignatureKeyNotFoundException)
        {
            return result;
        }

        // The token was signed with a key that is not in the cached set, which
        // is what a rotation looks like from here: refetch once before saying no.
        var refreshedKeys = await keyProvider
            .GetAsync(forceRefresh: true)
            .ConfigureAwait(false);

        if (AreSame(keys, refreshedKeys))
        {
            return result;
        }

        return await _handler
            .ValidateTokenAsync(token, BuildValidationParameters(refreshedKeys))
            .ConfigureAwait(false);
    }

    private TokenValidationParameters BuildValidationParameters(IReadOnlyList<SecurityKey> keys)
    {
        return new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = _jwt.Issuer,

            // The audience is the id of the app the account logged into. It is
            // only worth checking when this api serves a single known app.
            ValidateAudience = _jwt.AppId.HasValue,
            ValidAudience = _jwt.AppId?.ToString(),

            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromSeconds(_jwt.ClockSkewInSeconds),

            ValidateIssuerSigningKey = true,
            IssuerSigningKeys = keys,
            ValidAlgorithms = [SecurityAlgorithms.RsaSha256]
        };
    }

    private static bool AreSame(
        IReadOnlyList<SecurityKey> keys,
        IReadOnlyList<SecurityKey> otherKeys)
    {
        return keys.Count == otherKeys.Count &&
            keys.Select(k => k.KeyId).SequenceEqual(otherKeys.Select(k => k.KeyId));
    }

    private static string RemoveScheme(string authorizationHeaderValue)
    {
        var value = authorizationHeaderValue.Trim();

        return value.StartsWith(BearerScheme, StringComparison.OrdinalIgnoreCase)
            ? value[BearerScheme.Length..].Trim()
            : value;
    }

    #region Errors
    private static CqAuthException BuildError(Exception? exception)
    {
        var (code, message, description) = exception switch
        {
            SecurityTokenExpiredException => (
                "TokenExpired",
                "Expired token",
                "The access token expired, get a new one with the refresh token (POST /sessions/refresh)"),

            SecurityTokenInvalidAudienceException => (
                "Unauthenticated",
                "Token of another app",
                "The access token was issued for a different app"),

            SecurityTokenInvalidIssuerException => (
                "Unauthenticated",
                "Token of another issuer",
                "The access token was not issued by the configured auth provider"),

            _ => (
                "Unauthenticated",
                "Invalid token",
                "The access token could not be validated")
        };

        return new CqAuthException(new CqAuthErrorApi
        {
            StatusCode = HttpStatusCode.Unauthorized,
            Code = code,
            Message = message,
            Description = description,
            Errors = new { }
        });
    }
    #endregion Errors

    #region Read
    /// <summary>
    /// The payload is deserialized straight from the encoded segment instead of
    /// going through the handler's claim accessors, which do not map nested
    /// objects consistently across versions.
    /// </summary>
    private static AccessTokenPayload? ReadPayload(JsonWebToken jwt)
    {
        try
        {
            var payload = Base64UrlEncoder.DecodeBytes(jwt.EncodedPayload);

            return JsonSerializer.Deserialize<AccessTokenPayload>(payload);
        }
        catch (Exception)
        {
            return null;
        }
    }

    /// <summary>
    /// Same shape GET /me answers, with one gap: the token carries the blob
    /// keys but not their urls, which are presigned by the provider and cannot
    /// be built here. <see cref="BlobRead.Key"/> is set,
    /// <see cref="BlobRead.Url"/> is not.
    /// </summary>
    private static AccountLogged BuildAccountLogged(
        AccessTokenPayload payload,
        string token)
    {
        return new AccountLogged
        {
            Id = payload.AccountId,
            Email = payload.Email!,
            FirstName = payload.FirstName!,
            LastName = payload.LastName!,
            FullName = payload.FullName!,
            Locale = payload.Locale!,
            TimeZone = payload.TimeZone!,
            ProfilePicture = BuildBlob(payload.ProfilePictureKey),
            Tenant = BuildTenant(payload.Tenant),
            AppLogged = BuildApp(payload.AppLogged),
            Roles = payload.Roles.ConvertAll(r => r.Name!),
            Permissions = payload.Roles
                .SelectMany(r => r.Permissions)
                .Select(p => p.Key!)
                .ToList(),

            // The header value the rest of the SDK sends back to the provider,
            // built the same way GET /me returns it.
            Token = $"Bearer {token}"
        };
    }

    private static BlobRead? BuildBlob(string? key)
    {
        return string.IsNullOrWhiteSpace(key)
            ? null
            : new BlobRead { Key = key };
    }

    private static Tenant BuildTenant(TenantClaim? claim)
    {
        return new Tenant
        {
            Id = claim?.Id ?? Guid.Empty,
            Name = claim?.Name!,
            MiniLogo = BuildBlob(claim?.MiniLogoKey),
            CoverLogo = BuildBlob(claim?.CoverLogoKey),
            WebUrl = claim?.WebUrl
        };
    }

    private static AppBasicInfo BuildApp(AppClaim? claim)
    {
        return new AppBasicInfo
        {
            Id = claim?.Id ?? Guid.Empty,
            Name = claim?.Name!
        };
    }
    #endregion Read
}
