// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

using Ardalis.Result;
using Ardalis.SharedKernel;
using Bengbenz.Embassy.eServices.Core.CategoryAggregrate;
using Bengbenz.Embassy.eServices.Core.CategoryAggregrate.Specifications;

namespace Bengbenz.Embassy.eServices.UseCases.Categories.GetWithSubCategories;

/// <summary>
/// Handles the retrieval of a category and its subcategories based on the category's identifier.
/// </summary>
/// <param name="categoryRepository">The repository for accessing category data.</param>
public sealed class GetCategoryWithSubCategoriesHandler(IReadRepository<Category> categoryRepository)
  : IQueryHandler<GetCategoryWithAllSubCategoriesQuery, Result<Category>>
{
  /// <inheritdoc />
  public async Task<Result<Category>> Handle(GetCategoryWithAllSubCategoriesQuery request, CancellationToken cancellationToken)
  {
    var spec = new CategoryByIdWithSubCategoriesSpec(request.CategoryId);
    var entity = await categoryRepository.FirstOrDefaultAsync(spec, cancellationToken);
    if (entity is null) return Result.NotFound();

    return Result.Success(entity);
  }
}
