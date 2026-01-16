using CQ.ApiElements.AppConfig;
using CQ.AuthProvider.SDK.Accounts;
using CQ.AuthProvider.SDK.Apps;
using CQ.AuthProvider.SDK.Constants;
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
            .Configure<ConstantsAuthProviderSection>(config =>
            {
                config.ClientOwnerRoleId = Guid.Parse("01e55142-6b8c-4e7e-9d71-1e459d07796d");
            })
            ;

        var isFake = configuration.GetSection("Authentication:Fake:IsActive").Get<bool>();
        if (isFake)
        {
            services
                .AddFakeAuthentication<AccountLogged>(configuration, environment, fakeAuthenticationLifeTime: LifeTime.Transient)
                .AddService<IMeService, FakeMeService>(LifeTime.Transient)
                .AddService<IAccountService, FakeAccountService>(LifeTime.Transient)
                .AddService<ISessionService, FakeSessionService>(LifeTime.Transient)
                .AddService<IAppService, FakeAppService>(LifeTime.Transient)
            ;

            return services;
        }

        services
            .AddService<AuthProviderClient>(LifeTime.Transient)
            .AddService<IMeService, MeService>(LifeTime.Transient)
            .AddService<IAccountService, AccountService>(LifeTime.Transient)
            .AddService<ISessionService, SessionService>(LifeTime.Transient)
            .AddService<IHealthService, HealthService>(LifeTime.Transient)
            .AddService<IAppService, AppService>(LifeTime.Transient)
            ;


        return services;
    }

    public static IHealthChecksBuilder AddAuthProviderHealthCheck(
        this IHealthChecksBuilder healthChecksBuilder,
        IConfiguration configuration,
        IList<string>? tags = null)
    {
        var authProviderConfig = configuration.GetSection("Authentication:Fake:IsActive").Get<bool>();
        if (authProviderConfig)
        {
            return healthChecksBuilder.AddCheck<FakeAuthProviderServiceHealthCheck>("Auth Provider Web Api", tags: ["external", "auth-provider-health", ..(tags ?? [])]);
        }

        return healthChecksBuilder.AddCheck<AuthProviderServiceHealthCheck>("Auth Provider Web Api", tags: ["external", "auth-provider-health", ..(tags ?? [])]);
    }
}

