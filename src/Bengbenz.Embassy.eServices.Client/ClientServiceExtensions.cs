// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

using Bengbenz.Embassy.eServices.Client.Services;

namespace Bengbenz.Embassy.eServices.Client;

public static class ClientServiceExtensions
{
  public static IServiceCollection AddClientServices(this IServiceCollection services)
  {
    services.AddScoped<HttpService>();
    services.AddScoped<CategoryItemService>();
    
    //logger.LogInformation("{Project} services registered", "Client");
    return services;
  }
}
