// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

using Ardalis.Result;
using Ardalis.SharedKernel;
using AutoMapper;

namespace Bengbenz.Embassy.eServices.UseCases.Categories.List;

public sealed class ListCategoriesHandler(IListCategoriesQueryService queryService, IMapper mapper)
    : IQueryHandler<ListCategoriesQuery, Result<IEnumerable<CategoryDto>>>
{
    public async  Task<Result<IEnumerable<CategoryDto>>> Handle(ListCategoriesQuery query, CancellationToken cancellationToken)
    {
        var items = await queryService.GetCategoriesWithSubCategoriesAsync();
          
        IEnumerable<CategoryDto> result = items.Select(mapper.Map<CategoryDto>).ToList();
        return Result.Success(result);
    }
}
