// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

using Bengbenz.Embassy.eServices.UseCases.ServiceOffers;
using Bengbenz.Embassy.eServices.UseCases.ServiceOffers.Create;
using Bengbenz.Embassy.eServices.UseCases.ServiceOffers.Update;

namespace Bengbenz.Embassy.eServices.Client.Services;

public class ServiceOfferService(HttpService httpService, ILogger<ServiceOfferService> logger)
{
  private const string BaseWebApiUrl = "ServiceOffers";
  
  public async Task<List<ServiceOfferDto>> ListAsync()
  {
    logger.LogInformation("Fetching service offer items from API.");
    var itemList = await httpService.GetAsync<List<ServiceOfferDto>>(BaseWebApiUrl);
    logger.LogInformation("Service offer count: {responseCount}", itemList?.Count);
    return itemList!;
  }
  
  public async Task<ServiceOfferDto?> CreateAsync(CreateServiceOfferRequest request)
  {
      logger.LogInformation("Posting service offer item from API.");
     return await httpService.PostAsync<ServiceOfferDto>(BaseWebApiUrl, request);
  }
  
  public async Task<ServiceOfferDto?> EditAsync(UpdateServiceOfferRequest request)
  {
    return (await httpService.PutAsync<ServiceOfferDto>(BaseWebApiUrl, request));
  }

  public async Task<ServiceOfferDto?> DeleteAsync(int categoryItemId)
  {
    return (await httpService.DeleteAsync<ServiceOfferDto>(BaseWebApiUrl, categoryItemId));
  }

  public async Task<ServiceOfferDto?> GetByIdAsync(int id)
  {
    return await httpService.GetAsync<ServiceOfferDto>($"{BaseWebApiUrl}/{id}");
  }
}
