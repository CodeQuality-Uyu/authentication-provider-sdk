using CQ.AuthProvider.SDK.AppConfig;
using CQ.Extensions.ServiceCollection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var services = builder.Services;
var configuration = builder.Configuration;

var authSection = configuration
    .GetSection(AuthProviderSection.Name)
    .Get<AuthProviderSection>();

services
    .AddCqAuthServices(
    authSection,
    LifeTime.Scoped,
    LifeTime.Scoped,
    LifeTime.Scoped,
    LifeTime.Scoped,
    LifeTime.Scoped);

var app = builder.Build();

app.MapControllers();

app.Run();