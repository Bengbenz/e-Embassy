using Bengbenz.Embassy.eServices.Client;
using Bengbenz.Embassy.eServices.Core;
using Bengbenz.Embassy.eServices.Infrastructure;
using Bengbenz.Embassy.eServices.Infrastructure.Data;
using Bengbenz.Embassy.eServices.Infrastructure.Identity;
using Bengbenz.Embassy.eServices.Server.Components;
using Bengbenz.Embassy.eServices.Server.Components.Account;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using MudBlazor.Services;
using Serilog;
using Serilog.Extensions.Logging;

var logger = Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

logger.Information("Starting web host...");

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((_, config) => config.ReadFrom.Configuration(builder.Configuration));
var microsoftLogger = new SerilogLoggerFactory(logger)
    .CreateLogger<Program>();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddMudServices();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, PersistingServerAuthenticationStateProvider>();

builder.Services.AddAuthorization();
// builder.Services.AddAuthentication(options =>
//     {
//         options.DefaultScheme = IdentityConstants.ApplicationScheme;
//         options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
//     })
//     .AddIdentityCookies();

// Configuration for development environment to keep secrets out of source control.
if (builder.Environment.IsDevelopment())
{
    // ApplicationUser is used to look up the assembly that contains the user secrets.
    builder.Configuration.AddUserSecrets<ApplicationUser>();
}

// Add services from core project.
// Add services from infrastructure project.
builder.Services.AddCoreServices(microsoftLogger)
    .AddInfrastructureServices(
        microsoftLogger,
        builder.Configuration,
        builder.Environment.IsDevelopment());

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<AppIdentityDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(UserInfo).Assembly);

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

// Seed Database
await SeedDatabase(app);

app.Run();
return;


async Task SeedDatabase(WebApplication webApp)
{
    webApp.Logger.LogInformation("Seeding Database...");
    using var scope = webApp.Services.CreateScope();
    var scopedProvider = scope.ServiceProvider;
    try
    {
        var embassyDbContext = scopedProvider.GetRequiredService<EmbassyDbContext>();
        await EmbassyDbContextSeed.SeedAsync(embassyDbContext, webApp.Logger);

        var userManager = scopedProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = scopedProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var identityContext = scopedProvider.GetRequiredService<AppIdentityDbContext>();
        await AppIdentityDbContextSeed.SeedAsync(identityContext, webApp.Logger, userManager, roleManager);
    }
    catch (Exception ex)
    {
        webApp.Logger.LogError(ex, $"An error occurred seeding the DB. {ex.Message}");
    }
    webApp.Logger.LogInformation("Database's seeded.");
}

// Make the implicit Program.cs class public, so integration tests can reference the correct assembly for host building
public partial class Program;