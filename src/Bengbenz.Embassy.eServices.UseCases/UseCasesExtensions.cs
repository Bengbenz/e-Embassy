using Ardalis.SharedKernel;
using Bengbenz.Embassy.eServices.UseCases.Categories.List;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Bengbenz.Embassy.eServices.UseCases;

/// <summary>
/// Provides extension methods to IServiceCollection for registering use case services.
/// This class enhances the IServiceCollection by adding specific services related to the UseCases
/// domain, facilitating the centralization of service registration for this particular area of the application.
/// </summary>
public static class UseCasesExtensions

{
    // <summary>
    /// Registers services related to the UseCases domain into the IServiceCollection.
    /// </summary>
    /// <param name="services">The IServiceCollection instance to which the UseCases services will be added.</param>
    /// <param name="logger">A logger instance for logging the registration process.</param>
    /// <returns>The IServiceCollection instance with UseCases services registered, allowing for chaining of other service registration calls.</returns>
    public static IServiceCollection AddUseCasesServices(
    this IServiceCollection services,
    ILogger logger)
    {
        // services.AddScoped(typeof(IQueryHandler<,>), typeof(ListCategoriesHandler));
        logger.LogInformation("{Project} services registered", "UseCases");
        return services;
    }
}