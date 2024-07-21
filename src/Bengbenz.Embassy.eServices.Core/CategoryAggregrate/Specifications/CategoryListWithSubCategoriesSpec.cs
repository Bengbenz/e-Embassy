// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

using Ardalis.Specification;

namespace Bengbenz.Embassy.eServices.Core.CategoryAggregrate.Specifications;

public sealed class CategoryListWithSubCategoriesSpec : Specification<Category>
{
  public CategoryListWithSubCategoriesSpec()
  {
    Query
      .Where(c => c.ParentCategoryId == null)
      .Include(c => c.SubCategories);
  }
}
