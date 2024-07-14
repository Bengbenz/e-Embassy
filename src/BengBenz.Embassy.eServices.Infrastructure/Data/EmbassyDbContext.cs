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
public sealed class EmbassyDbContext(DbContextOptions<EmbassyDbContext> options) : DbContext(options);
