// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

using Ardalis.Specification;

namespace Bengbenz.Embassy.eServices.Core.CategoryAggregrate.Specifications;

/// <summary>
/// Specifies a condition to find a category by its unique identifier and includes its subcategories.
/// </summary>
/// <remarks>
/// This specification is used within queries to retrieve a specific category along with its subcategories,
/// facilitating operations that require a category's full hierarchy.
/// </remarks>
public sealed class CategoryByIdWithSubCategoriesSpec : Specification<Category>
{
  /// <summary>
  /// Initializes a new instance of the <see cref="CategoryByIdWithSubCategoriesSpec"/> class.
  /// </summary>
  /// <param name="categoryId">The unique identifier of the category to find.</param>
  public CategoryByIdWithSubCategoriesSpec(int categoryId)
  {
    Query
      .Where(c => c.Id == categoryId)
      .Include(c => c.SubCategories);
  }
}
