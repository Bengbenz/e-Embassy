using Bengbenz.Embassy.eServices.Core.Identity;
using Bengbenz.Embassy.eServices.Infrastructure.Identity.Config;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Bengbenz.Embassy.eServices.Infrastructure.Identity;

public static class AppIdentityDbContextSeed
{
    public static async Task SeedAsync(
        AppIdentityDbContext identityDbContext,
        ILogger logger,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        if (identityDbContext.Database.IsNpgsql())
        {
            await identityDbContext.Database.MigrateAsync();
        }

        await roleManager.CreateAsync(new IdentityRole(AuthorizationConstants.ADMINISTRATORS_ROLE));
        
        var defaultUser = new ApplicationUser { UserName = "user_beng", Email = "bengbenz@gmail.com" };
        await userManager.CreateAsync(defaultUser, AuthorizationConstants.DEFAULT_PASSWORD);
        
        string adminUserName = AuthorizationConstants.ADMINISTRATORS_USERNAME;
        var adminUser = new ApplicationUser { UserName = AuthorizationConstants.ADMINISTRATORS_USERNAME, Email = AuthorizationConstants.ADMINISTRATORS_EMAIL };
        await userManager.CreateAsync(adminUser, AuthorizationConstants.DEFAULT_PASSWORD);
        adminUser = await userManager.FindByNameAsync(adminUserName);
        if (adminUser is not null)
        {
            await userManager.AddToRoleAsync(adminUser, AuthorizationConstants.ADMINISTRATORS_ROLE);
        }
        await Task.CompletedTask;
        logger.LogInformation("Seed database associated with context {DbContextName}", nameof(AppIdentityDbContext));
    }
}
