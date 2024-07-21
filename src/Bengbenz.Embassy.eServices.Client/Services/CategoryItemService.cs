// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

using Bengbenz.Embassy.eServices.Client.Models.Categories;
using Bengbenz.Embassy.eServices.UseCases.Categories;

namespace Bengbenz.Embassy.eServices.Client.Services;

public class CategoryItemService(HttpService httpService, ILogger<CategoryItemService> logger)
{
  public async Task<List<CategoryDto>> ListAsync()
  {
    logger.LogInformation("Fetching category items from API.");
    var itemList = await httpService.GetAsync<List<CategoryDto>>($"categories");
    logger.LogInformation("Category count: {responseCount}", itemList?.Count);
    return itemList!;
  }
  
  public async Task<CategoryDto?> CreateAsync(CreateCategoryItemRequest request)
  {
    logger.LogInformation("Posting category item from API.");
     return await httpService.PostAsync<CategoryDto>($"categories", request);
  }
  
  public async Task<CategoryDto?> EditAsync(CategoryDto catalogItem)
  {
    return (await httpService.PutAsync<CategoryDto>("categories", catalogItem));
  }
}
