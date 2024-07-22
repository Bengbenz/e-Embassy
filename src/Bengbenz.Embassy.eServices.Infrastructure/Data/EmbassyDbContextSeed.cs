using Bengbenz.Embassy.eServices.Core.CategoryAggregrate;
using Bengbenz.Embassy.eServices.Core.ServiceOfferAggregrate;
using Bengbenz.Embassy.eServices.Infrastructure.Identity.Config;
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
            
            if (!await embassyContext.ServiceOffers.AnyAsync())
            {
              await embassyContext.ServiceOffers.AddRangeAsync(
                GetPreconfiguredServices());
            
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
    
    static IReadOnlyCollection<ServiceOffer> GetPreconfiguredServices()
    {
      var services = new List<ServiceOffer>
      {
        new("Certificat de coutume par le nom", "Certificat de coutume par le nom",  33,AuthorizationConstants.ADMINISTRATORS_USERNAME),
        new("Attestation de mariage", "Attestation de mariage",  45, AuthorizationConstants.ADMINISTRATORS_USERNAME),
        new("Attestation de célibat", "Attestation de célibat",  50, AuthorizationConstants.ADMINISTRATORS_USERNAME),
        new("Attestation de non-dissolution de PACS", "Attestation de non-dissolution de PACS", 65,AuthorizationConstants.ADMINISTRATORS_USERNAME),
      };

      return services;
    }
}
