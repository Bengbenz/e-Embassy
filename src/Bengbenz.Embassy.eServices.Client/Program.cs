using Bengbenz.Embassy.eServices.Client;
using Bengbenz.Embassy.eServices.UseCases;
using Bengbenz.Embassy.eServices.UseCases.Categories.Create;
using Bengbenz.Embassy.eServices.UseCases.Categories.Update;
using FluentValidation;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices(config =>
{
  config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;

  config.SnackbarConfiguration.PreventDuplicates = false;
  config.SnackbarConfiguration.NewestOnTop = true;
  config.SnackbarConfiguration.ShowCloseIcon = true;
  config.SnackbarConfiguration.VisibleStateDuration = 10000;
  config.SnackbarConfiguration.HideTransitionDuration = 500;
  config.SnackbarConfiguration.ShowTransitionDuration = 500;
  config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
});

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();


builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri($"{builder.HostEnvironment.BaseAddress}api") });
builder.Services.AddClientServices();

//builder.Services.AddValidatorsFromAssemblyContaining<UseCasesExtensions>();
builder.Services.AddScoped<CreateCategoryRequestValidator>();
builder.Services.AddScoped<UpdateCategoryRequestValidator>();


builder.Logging.AddConfiguration(builder.Configuration.GetRequiredSection("Logging"));

await builder.Build().RunAsync();
