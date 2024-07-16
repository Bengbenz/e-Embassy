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
            // await identityDbContext.Database.EnsureCreatedAsync();
            await identityDbContext.Database.MigrateAsync();
        }

        // await roleManager.CreateAsync(new IdentityRole(BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS));
        //
        // var defaultUser = new ApplicationUser { UserName = "demouser@microsoft.com", Email = "demouser@microsoft.com" };
        // await userManager.CreateAsync(defaultUser, AuthorizationConstants.DEFAULT_PASSWORD);
        //
        // string adminUserName = "admin@microsoft.com";
        // var adminUser = new ApplicationUser { UserName = adminUserName, Email = adminUserName };
        // await userManager.CreateAsync(adminUser, AuthorizationConstants.DEFAULT_PASSWORD);
        // adminUser = await userManager.FindByNameAsync(adminUserName);
        // if (adminUser is not null)
        // {
        //     await userManager.AddToRoleAsync(adminUser, BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS);
        // }
        await Task.CompletedTask;
        logger.LogInformation("Seed database associated with context {DbContextName}", nameof(AppIdentityDbContext));
    }
}