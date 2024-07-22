using Bengbenz.Embassy.eServices.Core.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bengbenz.Embassy.eServices.Infrastructure.Identity;

/// <summary>
/// Represents the database context for the application's identity system, extending the IdentityDbContext with ApplicationUser.
/// This context manages the identity-related data models such as users, roles, claims, etc., specific to the application's requirements.
/// </summary>
/// <remarks>
/// This class is sealed to prevent inheritance, ensuring the context's integrity and the specific configuration it encapsulates.
/// It is configured to use the options pattern for dependency injection of the DbContextOptions, allowing for flexible configuration.
/// </remarks>
public sealed class AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options)
    : IdentityDbContext<ApplicationUser>(options);