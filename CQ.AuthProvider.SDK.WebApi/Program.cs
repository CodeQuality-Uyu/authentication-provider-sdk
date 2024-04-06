using CQ.AuthProvider.SDK.AppConfig;
using CQ.ServiceExtension;

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

var app = builder.Build();

app.MapControllers();

app.Run();