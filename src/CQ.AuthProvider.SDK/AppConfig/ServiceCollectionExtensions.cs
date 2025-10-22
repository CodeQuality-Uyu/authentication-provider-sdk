﻿using CQ.ApiElements.AppConfig;
using CQ.AuthProvider.SDK.Accounts;
using CQ.AuthProvider.SDK.Health;
using CQ.AuthProvider.SDK.Http;
using CQ.AuthProvider.SDK.Me;
using CQ.AuthProvider.SDK.Sessions;
using CQ.Extensions.ServiceCollection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CQ.AuthProvider.SDK.AppConfig;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigAuthProviderSDK(
        this IServiceCollection services,
        IConfiguration configuration,
        IHostEnvironment environment)
    {
        var authProviderSection = configuration.GetRequiredSection(AuthProviderSection.Name);

        services
            .Configure<AuthProviderSection>(authProviderSection)
            .AddFakeAuthentication<AccountLogged>(configuration, environment, fakeAuthenticationLifeTime: LifeTime.Transient)
            .AddService<AuthProviderConnectionApi>(LifeTime.Transient)
            
            .AddService<IMeService, MeService>(LifeTime.Transient)
            .AddService<IAccountService, AccountService>(LifeTime.Transient)
            .AddService<ISessionService, SessionService>(LifeTime.Transient)
            .AddService<IHealthService, HealthService>(LifeTime.Transient)
            ;

        return services;
    }
}
