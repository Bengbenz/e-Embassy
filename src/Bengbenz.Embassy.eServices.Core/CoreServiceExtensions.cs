using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Bengbenz.Embassy.eServices.Core;

/// <summary>
/// Provides extension methods to IServiceCollection for registering core services.
/// This class enhances IServiceCollection by adding essential services for the application's core layer,
/// facilitating the centralization of service registration for core functionalities and components.
/// </summary>
public static class CoreServiceExtensions
{
    /// <summary>
    /// Adds core-related services to the IServiceCollection.
    /// This method is responsible for configuring and registering services that are crucial for the application's core functionality,
    /// such as domain services, application-wide configurations, and other foundational services.
    /// </summary>
    /// <param name="services">The IServiceCollection to add services to.</param>
    /// <param name="logger">A logger instance for logging the registration process.</param>
    /// <returns>The IServiceCollection with core services registered, allowing for chaining of other service registration calls.</returns>
    public static IServiceCollection AddCoreServices(
        this IServiceCollection services,
        ILogger logger)
    {
        logger.LogInformation("{Project} services registered", "Core");
        return services;
    }
}