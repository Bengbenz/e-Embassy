using Bengbenz.Embassy.eServices.Core.CategoryAggregrate;
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
                await embassyContext.Database.MigrateAsync();
            }

            if (!await embassyContext.Categories.AnyAsync())
            {
                await embassyContext.Categories.AddRangeAsync(
                    GetPreconfiguredCategories());
            
                await embassyContext.SaveChangesAsync();
            }
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

    static IReadOnlyCollection<Category> GetPreconfiguredCategories()
    {
        var subCategories = new List<Category>
        {
            new("Certificat"),
            new("Attestation"),
            new("Légalisation")
        };
        
        return new List<Category>
        {
            new( "Services aux nationaux",  subCategories),
            new("Services aux étrangers")
        };
    }
}