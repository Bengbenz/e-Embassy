// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

using Ardalis.Result;
using Ardalis.SharedKernel;
using Bengbenz.Embassy.eServices.Core.CategoryAggregrate;

namespace Bengbenz.Embassy.eServices.UseCases.Categories.List;

/// <summary>
/// Handles the operation of listing root categories along with their subcategories.
/// </summary>
/// <remarks>
/// This handler is responsible for executing the query to retrieve root categories and their subcategories,
/// leveraging the <see cref="IListCategoriesQueryService"/> to access category data.
/// It encapsulates the logic necessary to process the <see cref="ListRootCategoriesQuery"/> and return the result.
/// </remarks>
public sealed class ListRootCategoriesHandler(IListCategoriesQueryService queryService)
    : IQueryHandler<ListRootCategoriesQuery, Result<IEnumerable<Category>>>
{
     /// <inheritdoc />
    public async  Task<Result<IEnumerable<Category>>> Handle(ListRootCategoriesQuery query, CancellationToken cancellationToken)
    {
        var items = await queryService.GetRootCategoriesWithSubCategoriesAsync();
        
        return Result.Success(items);
    }
}
