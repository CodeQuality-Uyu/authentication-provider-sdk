using CQ.Extensions.Configuration;
using CQ.Extensions.ServiceCollection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CQ.AuthProvider.SDK.ApiFilters.AppConfig;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigAuthProviderApi(
        this IServiceCollection services,
        IConfiguration configuration,
        LifeTime authProviderApiServiceLifeTime = LifeTime.Transient,
        LifeTime authProviderSectionLifeTime = LifeTime.Singleton)
    {
        var authProviderSection = configuration.GetSection<AuthProviderSection>(AuthProviderSection.Name);

        services
            .AddService(authProviderSection, authProviderSectionLifeTime)
            .AddService<IAuthProviderConnection, AuthProviderConnectionApi>(authProviderApiServiceLifeTime);

        return services;
    }
}
