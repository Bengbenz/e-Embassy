using Ardalis.SharedKernel;
using Bengbenz.Embassy.eServices.Core.CategoryAggregrate;
using Microsoft.EntityFrameworkCore;

namespace Bengbenz.Embassy.eServices.Infrastructure.Data;

/// <summary>
/// Represents the primary database context for the Embassy eServices application.
/// This context is responsible for managing the application's core data models beyond identity, 
/// such as application-specific entities, configurations, and relationships.
/// </summary>
/// <remarks>
/// Similar to AppIdentityDbContext, this class might extend DbContext directly or a specialized version thereof,
/// depending on the needs for entity configuration and database interactions.
/// It encapsulates the application's data access logic, serving as a bridge between the application's domain models and the database.
/// This context is typically used for CRUD operations, querying, and transaction management within the application.
/// </remarks>
public sealed class EmbassyDbContext(DbContextOptions<EmbassyDbContext> options) : DbContext(options)
{
    private readonly IDomainEventDispatcher? _domainEventDispatcher;
    
    public DbSet<Category> Categories => Set<Category>();

    public EmbassyDbContext(
        DbContextOptions<EmbassyDbContext> options,
        IDomainEventDispatcher dispatcher) : this(options)
    {
        _domainEventDispatcher = dispatcher;
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Configure entity mappings and relationships.
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EmbassyDbContext).Assembly);
    }
    
/// <summary>
/// Asynchronously saves all changes made in this context to the database.
/// </summary>
/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
/// <returns>A task that represents the asynchronous save operation. The task result contains the number of state entries written to the database.</returns>
/// <remarks>
/// This method overrides the <see cref="DbContext.SaveChangesAsync(CancellationToken)"/> method to add additional functionality:
/// - Before saving changes, it checks for domain events in entities that inherit from <see cref="EntityBase"/>.
/// - If a <see cref="IDomainEventDispatcher"/> is provided, it dispatches all domain events for entities with changes being saved.
/// - Domain events are dispatched only if the save operation is successful, ensuring that events are only processed for successfully persisted changes.
/// - This method ensures that the application's domain events are consistently dispatched after changes are successfully saved to the database,
///   allowing for a clean separation of concerns and adherence to the transactional consistency principle.
/// </remarks>
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        var result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);    
        
        // ignore events if no dispatcher provided
        if (_domainEventDispatcher is null) return result;

        // dispatch events only if save was successful
        var entitiesWithEvents = ChangeTracker.Entries<EntityBase>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Any())
            .ToArray();

        await _domainEventDispatcher.DispatchAndClearEvents(entitiesWithEvents);

        return result;
    }
    
    /// <summary>
    ///  Synchronously saves all changes made in this context to the database. <see cref="SaveChangesAsync" />
    /// </summary>
    public override int SaveChanges() =>
        SaveChangesAsync().GetAwaiter().GetResult();
}