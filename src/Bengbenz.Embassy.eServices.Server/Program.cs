using System.Reflection;
using Ardalis.ListStartupServices;
using Bengbenz.Embassy.eServices.Client;
using Bengbenz.Embassy.eServices.Client.Shared;
using Bengbenz.Embassy.eServices.Core;
using Bengbenz.Embassy.eServices.Infrastructure;
using Bengbenz.Embassy.eServices.Infrastructure.Data;
using Bengbenz.Embassy.eServices.Infrastructure.Identity;
using Bengbenz.Embassy.eServices.Server.Components;
using Bengbenz.Embassy.eServices.Server.Components.Account;
using Bengbenz.Embassy.eServices.Server.Middleware;
using Bengbenz.Embassy.eServices.UseCases;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
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

//   *****
// Add services to the container.
//  *****
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

// Add web api controllers services.
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
builder.Configuration.AddEnvironmentVariables();

// add list services for diagnostic purposes - see https://github.com/ardalis/AspNetCoreStartupServices
builder.Services.Configure<ServiceConfig>(config =>
{
  config.Services = new List<ServiceDescriptor>(builder.Services);

  // optional - default path to view services is /listallservices - recommended to choose your own path
  config.Path = "/servicesinfo";
});

// Add swagger services.
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Bengbenz.Embassy.eServices API",
        Description = "A Web API for managing e-public services at CAR Embassy",
        //TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Caleb BENGUELET",
            //Url = new Uri("bengbenz@gmail.com")
        },
        License = new OpenApiLicense
        {
            Name = "MIT License",
            //Url = new Uri("https://github.com/Bengbenz/e-Embassy?tab=MIT-1-ov-file")
        }
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

// Blazor wasm client configuration
var baseUrlConfig = builder.Configuration.GetSection(BaseUrlConfiguration.CONFIG_NAME)
  .Get<BaseUrlConfiguration>();
    
// Blazor wasm client required Services for Prerendering
builder.Services.AddScoped<HttpClient>(s => new HttpClient
{
  BaseAddress = new Uri(baseUrlConfig!.WebBase ?? string.Empty)
});

// Add services from core project.
// Add services from use cases project.
// Add services from infrastructure project.
builder.Services.AddCoreServices(microsoftLogger)
  .AddUseCasesServices(microsoftLogger)
  .AddInfrastructureServices(
    microsoftLogger,
    builder.Configuration,
    builder.Environment.IsDevelopment())
  .AddClientServices();
 
// Add MediatR services.
builder.Services.AddMediatRServices();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services
    .AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<AppIdentityDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

var app = builder.Build();

// *****
// Configure the HTTP request pipeline (Middleware).
// *****
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseWebAssemblyDebugging();
    app.UseMigrationsEndPoint();
    app.UseSwagger();
    app.UseSwaggerUI();
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
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Add Blazor web assembly middleware
app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(UserInfo).Assembly);

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.MapControllers();

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
