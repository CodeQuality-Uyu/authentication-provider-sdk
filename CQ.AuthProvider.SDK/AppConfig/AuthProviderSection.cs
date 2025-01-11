﻿namespace CQ.AuthProvider.SDK.AppConfig;

public sealed record class AuthProviderSection
{
    public const string Name = "Authentication";

    public string Server { get; init; } = null!;

    public FakeOptions Fake { get; init; } = null!;
}

public sealed record class FakeOptions
{
    public bool IsActive { get; init; }

    public AccountFake? Account { get; init; }
}

public sealed record class AccountFake
{
    public Guid Id { get; init; }

    public string FirstName { get; init; } = null!;

    public string LastName { get; init; } = null!;

    public string FullName { get; init; } = null!;

    public string Email { get; init; } = null!;

    public string Locale { get; init; } = null!;

    public string TimeZone { get; init; } = null!;

    public List<string> Permissions { get; init; } = [];

    public List<string> Roles { get; init; } = [];
}
