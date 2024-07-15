using Bengbenz.Embassy.eServices.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Bengbenz.Embassy.eServices.Infrastructure.Data.Config;
using Bengbenz.Embassy.eServices.Infrastructure.Identity;
using Bengbenz.Embassy.eServices.Infrastructure.Identity.Config;

namespace Bengbenz.Embassy.eServices.IntegrationTests.Infrastructure.Migrations;

public class ModelMigrationsShould
{
    [Fact]
    public void Ensure_EmbassyDbModel_IsInSyncWithDatabase()
    {
        // Arrange
        var context = new EmbassyDbDesignTimeFactory().CreateDbContext([]);
        
        // Act
        var hasPendingModelChanges = context.Database.HasPendingModelChanges();
        
        // Assert
        Assert.False(hasPendingModelChanges, $"There are pending model in '{nameof(EmbassyDbContext)}' changes that have not been applied via migrations.");
    }
    
    [Fact]
    public void Ensure_AppIdentityDbModel_IsInSyncWithDatabase()
    {
        // Arrange
        var context = new AppIdentityDbDesignTimeFactory().CreateDbContext([]);
        
        // Act
        var hasPendingModelChanges = context.Database.HasPendingModelChanges();
        
        // Assert
        Assert.False(hasPendingModelChanges, $"There are pending model in '{nameof(AppIdentityDbContext)}' changes that have not been applied via migrations.");
    }
}