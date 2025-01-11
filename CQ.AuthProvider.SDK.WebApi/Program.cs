using CQ.AuthProvider.SDK.ApiFilters.AppConfig;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var services = builder.Services;
var configuration = builder.Configuration;

services.ConfigAuthProviderApi(configuration);

var app = builder.Build();

app.MapControllers();

app.Run();