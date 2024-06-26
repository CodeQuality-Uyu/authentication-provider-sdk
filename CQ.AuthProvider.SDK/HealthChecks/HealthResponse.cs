﻿
namespace CQ.AuthProvider.SDK.HealthChecks;
internal sealed record class HealthResponse
{
    public bool IsActive { get; init; }

    public AuthProviderResponse Auth { get; init; } = null!;
}

internal sealed record class AuthProviderResponse
{
    public string Type { get; init; } = null!;

    public bool IsActive { get; init; }
}
