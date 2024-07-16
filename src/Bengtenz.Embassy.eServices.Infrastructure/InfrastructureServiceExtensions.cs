using Ardalis.SharedKernel;
using Bengbenz.Embassy.eServices.Infrastructure.Data;
using Bengbenz.Embassy.eServices.Infrastructure.Data.Queries;
using Bengbenz.Embassy.eServices.Infrastructure.Identity;
using Bengbenz.Embassy.eServices.UseCases.Categories.List;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Bengbenz.Embassy.eServices.Infrastructure;

/// <summary>
/// Provides extension methods to IServiceCollection for registering infrastructure services.
/// This class enhances IServiceCollection by adding essential services for the application's infrastructure layer,
/// including environment-specific dependencies, Entity Framework Core configurations, and identity services.
/// </summary>
public static class InfrastructureServiceExtensions
{
    /// <summary>
    /// Adds infrastructure-related services to the IServiceCollection.
    /// This method configures services based on the application's running environment (development or production),
    /// sets up Entity Framework Core with PostgreSQL, and configures identity services. It differentiates between
    /// development and production environments to register environment-specific dependencies. For Entity Framework Core,
    /// it configures the application's DbContext with a PostgreSQL database connection. Identity services are set up
    /// with default configurations and linked to the application's DbContext for user management.
    /// </summary>
    /// <param name="services">The IServiceCollection to add services to.</param>
    /// <param name="logger">A logger instance for logging the registration process.</param>
    /// <param name="configuration">The application's configuration, used to retrieve the database connection string and other settings.</param>
    /// <param name="isDevelopment">A boolean indicating if the application is running in a development environment. This affects the registration of certain services.</param>
    /// <returns>The IServiceCollection with infrastructure services registered, allowing for chaining of other service registration calls.</returns>
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        ILogger logger,
        IConfiguration configuration,
        bool isDevelopment)
    {
        RegisterEntityFrameworkServices(services, configuration);
        
        if (isDevelopment)
        {
            RegisterDevelopmentOnlyDependencies(services);
        }
        else
        {
            RegisterProductionOnlyDependencies(services);
        }

        logger.LogInformation("{Project} services registered", "Infrastructure");
        
        return services;
    }
    
    /// <summary>
    /// Registers services that are only needed in a development environment.
    /// </summary>
    /// <param name="services">The IServiceCollection to add development-only services to.</param>
    private static void RegisterDevelopmentOnlyDependencies(IServiceCollection services)
    {
        // services.AddScoped<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();
        // services.AddScoped<IListContributorsQueryService, FakeListContributorsQueryService>();
        // services.AddScoped<IListIncompleteItemsQueryService, FakeListIncompleteItemsQueryService>();
        // services.AddScoped<IListProjectsShallowQueryService, FakeListProjectsShallowQueryService>();
    }

    /// <summary>
    /// Registers services that are only needed in a production environment.
    /// </summary>
    /// <param name="services">The IServiceCollection to add production-only services to.</param>
    private static void RegisterProductionOnlyDependencies(IServiceCollection services)
    {
        // services.AddScoped<IEmailSender<ApplicationUser>, SmtpEmailSender>();
        services.AddScoped<IListCategoriesQueryService, ListCategoriesQueryService>();
        // services.AddScoped<IListIncompleteItemsQueryService, ListIncompleteItemsQueryService>();
        // services.AddScoped<IListProjectsShallowQueryService, ListProjectsShallowQueryService>();
    }

    /// <summary>
    /// Registers Entity Framework Core services and configurations. This includes setting up the main application's DbContext
    /// with either a PostgreSQL database or an in-memory database based on the application's configuration. This flexibility
    /// supports different scenarios, such as testing with an in-memory database or running against a PostgreSQL database in production.
    /// Repository patterns are also registered to facilitate easier data access and manipulation across the application.
    /// </summary>
    /// <param name="services">The IServiceCollection to add EF Core services to.</param>
    /// <param name="configuration">The application's configuration, used to determine whether to use an in-memory database and to retrieve the database connection string.</param>
    private static void RegisterEntityFrameworkServices(
        IServiceCollection services,
        IConfiguration configuration)
    {
        bool isUsedOnlyInMemoryDatabase = false;
        if (configuration["UseOnlyInMemoryDatabase"] is not null)
        {
            isUsedOnlyInMemoryDatabase = bool.Parse(configuration["UseOnlyInMemoryDatabase"]!);
        }
        
        var defaultConnectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        var identityConnectionString = configuration.GetConnectionString("IdentityConnection") ?? throw new InvalidOperationException("Connection string 'IdentityConnection' not found.");
        
        services.AddDbContext<EmbassyDbContext>(options =>
        {
            _ = isUsedOnlyInMemoryDatabase
                ? options.UseInMemoryDatabase(defaultConnectionString)
                : options.UseNpgsql(defaultConnectionString);
        });
            
        services.AddDbContext<AppIdentityDbContext>(options =>
        {
            _ = isUsedOnlyInMemoryDatabase
                ? options.UseInMemoryDatabase(identityConnectionString)
                : options.UseNpgsql(identityConnectionString);
        });
        
        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
        services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));
    }
}