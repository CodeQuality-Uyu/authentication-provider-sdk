using CQ.AuthProvider.SDK.ApiFilters.Accounts;
using CQ.Extensions.ServiceCollection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CQ.AuthProvider.SDK.ApiFilters.AppConfig;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigAuthProviderApi(
        this IServiceCollection services,
        IConfiguration configuration,
        LifeTime authProviderApiServiceLifeTime = LifeTime.Scoped)
    {
        var authProviderSection = configuration.GetRequiredSection("Authentication");

        services
            .Configure<AuthProviderSection>(authProviderSection)
            .AddFakeAuthentication<AccountLogged>(configuration)
            .AddService<IAuthProviderConnection, AuthProviderConnectionApi>(authProviderApiServiceLifeTime);

        return services;
    }
}
