// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

using Bengbenz.Embassy.eServices.UseCases.Categories;
using Bengbenz.Embassy.eServices.UseCases.Categories.Create;
using Bengbenz.Embassy.eServices.UseCases.Categories.Update;

namespace Bengbenz.Embassy.eServices.Client.Services;

/// <summary>
/// Provides services for managing category items through HTTP requests.
/// </summary>
/// <remarks>
/// This service class encapsulates the logic for making HTTP requests to manage category items, including
/// listing all categories, creating a new category, editing an existing category, deleting a category, and
/// fetching a category by its ID. It utilizes an <see cref="HttpService"/> for the actual HTTP calls and logs
/// operations using an <see cref="ILogger"/>.
/// </remarks>
public class CategoryItemService(HttpService httpService, ILogger<CategoryItemService> logger)
{
  private const string BaseWebApiUrl = "Categories";
  
  /// <summary>
  /// Lists all root category items.
  /// </summary>
  /// <returns>A list of <see cref="CategoryDto"/> root categories objects.</returns>
  public async Task<List<CategoryDto>> ListAsync()
  {
    logger.LogInformation("Fetching category items from API.");
    var itemList = await httpService.GetAsync<List<CategoryDto>>(BaseWebApiUrl);
    logger.LogInformation("Category count: {responseCount}", itemList?.Count);
    return itemList!;
  }
  
  /// <summary>
  /// Creates a new category item.
  /// </summary>
  /// <param name="request">The request containing the details of the category to create.</param>
  /// <returns>The created <see cref="CategoryDto"/> object, or null if the operation fails.</returns>
  public async Task<CategoryDto?> CreateAsync(CreateCategoryRequest request)
  {
      logger.LogInformation("Posting category item from API.");
     return await httpService.PostAsync<CategoryDto>(BaseWebApiUrl, request);
  }
  
  /// <summary>
  /// Edits an existing category item.
  /// </summary>
  /// <param name="request">The request containing the new details of the category.</param>
  /// <returns>The updated <see cref="CategoryDto"/> object, or null if the operation fails.</returns>
  public async Task<CategoryDto?> EditAsync(UpdateCategoryRequest request)
  {
    return (await httpService.PutAsync<CategoryDto>(BaseWebApiUrl, request));
  }
  
  /// <summary>
  /// Deletes a category item by its unique identifier.
  /// </summary>
  /// <param name="categoryItemId">The unique identifier of the category to delete.</param>
  /// <returns>The <see cref="CategoryDto"/> object of the deleted category, or null if the operation fails.</returns>
  public async Task<CategoryDto?> DeleteAsync(int categoryItemId)
  {
    return (await httpService.DeleteAsync<CategoryDto>(BaseWebApiUrl, categoryItemId));
  }
  
  /// <summary>
  /// Fetches a category item by its unique identifier.
  /// </summary>
  /// <param name="id">The unique identifier of the category to fetch.</param>
  /// <returns>The <see cref="CategoryDto"/> object of the fetched category, or null if the operation fails.</returns>
  public async Task<CategoryDto?> GetByIdAsync(int id)
  {
    return await httpService.GetAsync<CategoryDto>($"{BaseWebApiUrl}/{id}");
  }
}
