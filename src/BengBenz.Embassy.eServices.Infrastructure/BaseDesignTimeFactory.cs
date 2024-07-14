using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Bengbenz.Embassy.eServices.Infrastructure;

/// <summary>
/// Provides a base implementation for an IDesignTimeDbContextFactory that creates DbContext instances at design time.
/// This abstract class is intended to be inherited by concrete factory classes that specify the DbContext type.
/// This is useful for EF Core migrations and tooling, especially when the DbContext configuration isn't directly available at runtime.
/// </summary>
/// <typeparam name="T">The type of the DbContext.</typeparam>
public abstract class BaseDesignTimeFactory<T> : IDesignTimeDbContextFactory<T> where T : DbContext
{
    /// <summary>
    /// An abstract property that derived classes must implement to specify the key used to retrieve the connection string from configuration.
    /// This allows different DbContexts to specify their own connection strings in the configuration.
    /// </summary>
    public T CreateDbContext(string[] args)
    {
        // Get environment
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
        
        // Build configuration
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(Path.Combine("../Bengbenz.Embassy.eServices.Server", "appsettings.json"), optional: true, reloadOnChange: true)
            .AddJsonFile(Path.Combine("../Bengbenz.Embassy.eServices.Server", $"appsettings.{environment}.json"), optional: true)
            .AddUserSecrets(typeof(BaseDesignTimeFactory<>).Assembly, optional: true)
            .AddEnvironmentVariables()
            .Build();
        
        // Create options builder
        var builder = new DbContextOptionsBuilder<T>();
        var connectionString = configuration.GetConnectionString(ConnectionStringKey)
                               ?? throw new InvalidOperationException($"Connection string '{ConnectionStringKey}' not found.");

        // Use PostgreSQL as the database provider
        builder.UseNpgsql(connectionString);

        // Return a new instance of T
        return (Activator.CreateInstance(typeof(T), builder.Options) as T)!;
    }

    /// <summary>
    /// An abstract property that derived classes must implement to specify the key used to retrieve the connection string from configuration.
    /// This allows different DbContexts to specify their own connection strings in the configuration.
    /// </summary>
    protected abstract string ConnectionStringKey { get; }
}