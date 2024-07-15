// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Bengbenz.Embassy.eServices.UseCases.Categories.List;

public sealed class ListCategoriesHandler(IListCategoriesQueryService queryService)
    : IQueryHandler<ListCategoriesQuery, Result<IEnumerable<CategoryDto>>>
{
    public async  Task<Result<IEnumerable<CategoryDto>>> Handle(ListCategoriesQuery query, CancellationToken cancellationToken)
    {
        var result = await queryService.ListAsync();
        return Result.Success(result);
    }
}