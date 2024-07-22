// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

using Bengbenz.Embassy.eServices.Core.CategoryAggregrate;

namespace Bengbenz.Embassy.eServices.UseCases.Categories.List;

/// <summary>
/// Defines the service for querying categories.
/// </summary>
public interface IListCategoriesQueryService
{
  /// <summary>
  /// Asynchronously retrieves a list of all categories.
  /// </summary>
  /// <returns>A task that represents the asynchronous operation. The task result contains a collection of <see cref="Category"/>.</returns>
  Task<IEnumerable<Category>> ListAsync();

  /// <summary>
  /// Asynchronously retrieves a list of root categories along with their subcategories.
  /// </summary>
  /// <returns>A task that represents the asynchronous operation. The task result contains a collection of <see cref="Category"/> including their subcategories.</returns>
  Task<IEnumerable<Category>> GetRootCategoriesWithSubCategoriesAsync();
}
