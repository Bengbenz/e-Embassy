// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

using Bengbenz.Embassy.eServices.Client.Services;
using Bengbenz.Embassy.eServices.UseCases.Categories.Create;
using Bengbenz.Embassy.eServices.UseCases.Categories.Update;
using Bengbenz.Embassy.eServices.UseCases.ServiceOffers.Create;

namespace Bengbenz.Embassy.eServices.Client;

public static class ClientServiceExtensions
{
  public static IServiceCollection AddClientServices(this IServiceCollection services)
  {
    services.AddScoped<HttpService>();
    services.AddScoped<CategoryItemService>();
    services.AddScoped<ServiceOfferService>();
    
    services.AddScoped<CreateCategoryRequestValidator>();
    services.AddScoped<UpdateCategoryRequestValidator>();
    services.AddScoped<CreateServiceOfferRequestValidator>();
    return services;
  }
}
