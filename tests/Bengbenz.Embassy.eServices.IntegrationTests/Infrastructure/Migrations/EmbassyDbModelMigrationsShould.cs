using Bengbenz.Embassy.eServices.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Bengbenz.Embassy.eServices.IntegrationTests.Infrastructure.Migrations;

public class EmbassyDbModelMigrationsShould : BaseEfRepoTestFixture<EmbassyDbContext>
{
    // [Fact]
    // public void Ensure_IsInSyncWithDatabase()
    // {
    //     // Act
    //     var hasPendingModelChanges = _dbContext.Database.HasPendingModelChanges();
    //     
    //     // Assert
    //     Assert.False(hasPendingModelChanges, $"There are pending model in '{nameof(EmbassyDbContext)}' changes that have not been applied via migrations.");
    // }
    
    // [Fact]
    // public void Ensure_AppIdentityDbModel_IsInSyncWithDatabase()
    // {
    //     // Arrange
    //     var context = new AppIdentityDbDesignTimeFactory().CreateDbContext([]);
    //     
    //     // Act
    //     var hasPendingModelChanges = context.Database.HasPendingModelChanges();
    //     
    //     // Assert
    //     Assert.False(hasPendingModelChanges, $"There are pending model in '{nameof(AppIdentityDbContext)}' changes that have not been applied via migrations.");
    // }
}