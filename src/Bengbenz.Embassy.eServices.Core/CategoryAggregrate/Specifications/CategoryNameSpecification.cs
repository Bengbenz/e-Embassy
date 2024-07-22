// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

using Ardalis.Specification;

namespace Bengbenz.Embassy.eServices.Core.CategoryAggregrate.Specifications;

/// <summary>
/// Specifies a condition to find a category by its name.
/// </summary>
/// <remarks>
/// This specification is used to filter categories based on a specific name,
/// ensuring that queries or operations can target categories with the exact name provided.
/// </remarks>
public sealed class CategoryNameSpecification : Specification<Category>
{
  /// <summary>
  /// Initializes a new instance of the <see cref="CategoryNameSpecification"/> class.
  /// </summary>
  /// <param name="name">The name of the category to find.</param>
  public CategoryNameSpecification(string name)
  {
    Query.Where(category => category.Name == name);
  }
}
