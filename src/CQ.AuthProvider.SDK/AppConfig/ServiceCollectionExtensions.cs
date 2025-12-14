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
            .AddFakeAuthentication<AccountLogged>(configuration, environment, fakeAuthenticationLifeTime: LifeTime.Transient)
            .AddService<AuthProviderClient>(LifeTime.Transient)

            .AddService<IMeService, MeService>(LifeTime.Transient)
            .AddService<IAccountService, AccountService>(LifeTime.Transient)
            .AddService<ISessionService, SessionService>(LifeTime.Transient)
            .AddService<IHealthService, HealthService>(LifeTime.Transient)
            .AddService<IAppService, AppService>(LifeTime.Transient)
            ;

        services.Configure<ConstantsAuthProviderSection>(config =>
        {
            config.ClientOwnerRoleId = Guid.Parse("01e55142-6b8c-4e7e-9d71-1e459d07796d");
        });

        return services;
    }

    public static IHealthChecksBuilder AddAuthProviderHealthCheck(
        this IHealthChecksBuilder healthChecksBuilder,
        IConfiguration configuration)
    {
        var authProviderConfig = configuration.GetSection("Authentication:Fake:IsActive").Get<bool>();
        if (authProviderConfig)
        {
            return healthChecksBuilder.AddCheck<AuthProviderServiceHealthCheck>("Auth Provider Web Api", tags: ["external", "auth-provider-health"]);;
        }

        return healthChecksBuilder.AddCheck<AuthProviderServiceHealthCheck>("Auth Provider Web Api", tags: ["external", "auth-provider-health"]);
    }
}

