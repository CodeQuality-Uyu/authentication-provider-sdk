
using CQ.Extensions.ServiceCollection;
using Microsoft.Extensions.DependencyInjection;

namespace CQ.AuthProvider.SDK.ApiFilters.AppConfig;

public static  class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigAuthProviderApi(
        this IServiceCollection services,
        LifeTime authProviderApiService = LifeTime.Transient)
    {
        services.AddService<IAuthProviderConnection, AuthProviderConnectionApi>(authProviderApiService);

        return services;
    }
}
