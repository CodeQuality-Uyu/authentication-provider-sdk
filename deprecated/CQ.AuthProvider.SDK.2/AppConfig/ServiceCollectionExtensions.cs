using CQ.AuthProvider.SDK.Abstractions.Accounts;
using CQ.AuthProvider.SDK.Abstractions.HealthCheck;
using CQ.AuthProvider.SDK.Abstractions.Sessions;
using CQ.AuthProvider.SDK.Abstractions.Users;
using CQ.AuthProvider.SDK.Accounts;
using CQ.AuthProvider.SDK.Accounts.Profiles;
using CQ.AuthProvider.SDK.AuthProviderConnections;
using CQ.AuthProvider.SDK.AuthProviderConnections.Api;
using CQ.AuthProvider.SDK.AuthProviderConnections.Fake;
using CQ.AuthProvider.SDK.HealthChecks;
using CQ.AuthProvider.SDK.Sessions;
using CQ.AuthProvider.SDK.Sessions.Profiles;
using CQ.Extensions.Configuration;
using CQ.Extensions.ServiceCollection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CQ.AuthProvider.SDK.AppConfig;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCqAuthServices(
        this IServiceCollection services,
        IConfiguration configuration,
        LifeTime authProviderSectionLifeTime,
        LifeTime authProviderConnectionLifeTime,
        LifeTime accountServiceLifeTime,
        LifeTime sessionServiceLifeTime,
        LifeTime healthServiceLifeTime)
    {
        var authProviderSection = configuration.GetSection<AuthProviderSection>(AuthProviderSection.Name);

        return services.AddCqAuthServices(
            authProviderSection,
            authProviderSectionLifeTime,
            authProviderConnectionLifeTime,
            accountServiceLifeTime,
            sessionServiceLifeTime,
            healthServiceLifeTime
            );
    }

    public static IServiceCollection AddCqAuthServices(
        this IServiceCollection services,
        AuthProviderSection authProviderSection,
        LifeTime authProviderSectionLifeTime,
        LifeTime authProviderConnectionLifeTime,
        LifeTime accountServiceLifeTime,
        LifeTime sessionServiceLifeTime,
        LifeTime healthServiceLifeTime)
    {
        services
            .AddAutoMapper(
            typeof(AccountProfile),
            typeof(SessionProfile));

        services.AddService(authProviderSection, authProviderSectionLifeTime);

        if (authProviderSection.Fake.IsActive)
        {
            services.AddService<IAuthProviderConnection, AuthProviderConnectionFake>(authProviderConnectionLifeTime);
        }
        else
        {
            services.AddService<IAuthProviderConnection, AuthProviderConnectionApi>(authProviderConnectionLifeTime);
        }

        services
            .AddService<IAuthHealthService, HealthService>(healthServiceLifeTime)
            .AddService<ISessionService, SessionService>(sessionServiceLifeTime)
            .AddService<IAccountService, AccountService>(accountServiceLifeTime)
            ;

        return services;
    }

    public static IServiceCollection AddUserLoggedService<TService>(
        this IServiceCollection services,
        LifeTime userServiceLifeTime)
        where TService : class, IUserService
    {
        services.AddService<IUserService, TService>(userServiceLifeTime);

        return services;
    }
}
