using CQ.AuthProvider.SDK.Accounts;
using CQ.AuthProvider.SDK.AppConfig;
using CQ.AuthProvider.SDK.Authorization;
using CQ.ServiceExtension;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var services = builder.Services;
var configuration = builder.Configuration;

var authSection = configuration
    .GetSection(AuthProviderOptions.AuthProvider)
    .Get<AuthProviderOptions>();

services
    .AddCqAuthService(
    authSection,
    LifeTime.Scoped,
    LifeTime.Scoped,
    LifeTime.Scoped,
    LifeTime.Scoped,
    LifeTime.Scoped,
    LifeTime.Scoped);

SeedRolesAndPermissionsAsync();

async Task SeedRolesAndPermissionsAsync()
{
    await services
        .AddRolesSeedDataAsync(new List<Role>
        {
        new Role(
            "User",
            "User",
            new RoleKey("user"),
            new List<PermissionKey>(),
            false,
            true) })
        .ConfigureAwait(false);
}

var app = builder.Build();

app.MapControllers();

app.Run();