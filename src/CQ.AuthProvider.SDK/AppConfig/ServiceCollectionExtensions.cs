using CQ.ApiElements.AppConfig;
using CQ.AuthProvider.SDK.Accounts;
using CQ.AuthProvider.SDK.Health;
using CQ.AuthProvider.SDK.Http;
using CQ.AuthProvider.SDK.Me;
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
        IHostEnvironment environment,
        LifeTime fakeAccountLoggedLifeTime = LifeTime.Scoped,
        LifeTime authProviderApiServiceLifeTime = LifeTime.Scoped)
    {
        var authProviderSection = configuration.GetRequiredSection(AuthProviderSection.Name);

        services
            .Configure<AuthProviderSection>(authProviderSection)
            .AddFakeAuthentication<AccountLogged>(configuration, environment, fakeAuthenticationLifeTime: fakeAccountLoggedLifeTime)
            .AddService<AuthProviderConnectionApi>(authProviderApiServiceLifeTime)
            
            .AddService<IMeService, MeService>(LifeTime.Scoped)
            .AddService<IAccountService, AccountService>(LifeTime.Scoped)
            .AddService<IHealthService, HealthService>(LifeTime.Scoped)
            ;

        return services;
    }
}
