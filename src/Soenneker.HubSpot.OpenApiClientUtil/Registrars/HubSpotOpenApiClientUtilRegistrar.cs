using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.HubSpot.Client.Registrars;
using Soenneker.HubSpot.OpenApiClientUtil.Abstract;

namespace Soenneker.HubSpot.OpenApiClientUtil.Registrars;

/// <summary>
/// Registers the cached HubSpot generated-client provider.
/// </summary>
public static class HubSpotOpenApiClientUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="HubSpotOpenApiClientUtil"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddHubSpotOpenApiClientUtilAsSingleton(this IServiceCollection services)
    {
        services.AddHubSpotClientUtilAsSingleton()
                .TryAddSingleton<IHubSpotOpenApiClientUtil, HubSpotOpenApiClientUtil>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="HubSpotOpenApiClientUtil"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddHubSpotOpenApiClientUtilAsScoped(this IServiceCollection services)
    {
        services.AddHubSpotClientUtilAsSingleton()
                .TryAddScoped<IHubSpotOpenApiClientUtil, HubSpotOpenApiClientUtil>();

        return services;
    }
}
