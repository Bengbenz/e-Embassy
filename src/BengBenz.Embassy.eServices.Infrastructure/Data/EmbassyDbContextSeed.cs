using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Bengbenz.Embassy.eServices.Infrastructure.Data;

public static class EmbassyDbContextSeed
{
    public static async Task SeedAsync(
        EmbassyDbContext embassyContext,
        ILogger logger,
        int retry = 0)
    {
        var retryForAvailability = retry;
        try
        {
            if (embassyContext.Database.IsNpgsql())
            {
                // await embassyContext.Database.EnsureCreatedAsync();
                await embassyContext.Database.MigrateAsync();
            }

            // if (!await embassyContext.CatalogBrands.AnyAsync())
            // {
            //     await embassyContext.CatalogBrands.AddRangeAsync(
            //         GetPreconfiguredCatalogBrands());
            //
            //     await embassyContext.SaveChangesAsync();
            // }
            //
            // if (!await embassyContext.CatalogTypes.AnyAsync())
            // {
            //     await embassyContext.CatalogTypes.AddRangeAsync(
            //         GetPreconfiguredCatalogTypes());
            //
            //     await embassyContext.SaveChangesAsync();
            // }
            //
            // if (!await embassyContext.CatalogItems.AnyAsync())
            // {
            //     await embassyContext.CatalogItems.AddRangeAsync(
            //         GetPreconfiguredItems());
            //
            //     await embassyContext.SaveChangesAsync();
            // }
            await Task.CompletedTask;
        }
        catch (Exception ex)
        {
            if (retryForAvailability >= 10) throw;
            retryForAvailability++;
            
            logger.LogError(ex.Message);
            await SeedAsync(embassyContext, logger, retryForAvailability);
            throw;
        }
        logger.LogInformation("Seed database associated with context {DbContextName}", nameof(EmbassyDbContext));
    }
}