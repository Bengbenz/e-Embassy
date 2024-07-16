// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

using Ardalis.SharedKernel;
using Bengbenz.Embassy.eServices.Core.CategoryAggregrate;
using Bengbenz.Embassy.eServices.UseCases.Categories;
using Bengbenz.Embassy.eServices.UseCases.Categories.List;

namespace Bengbenz.Embassy.eServices.Infrastructure.Data.Queries;

public sealed class ListCategoriesQueryService(IReadRepository<Category> repository) : IListCategoriesQueryService
{
    public async Task<IEnumerable<CategoryDto>> ListAsync()
    {
        var categories = await repository.ListAsync();
        return categories.Select(c => new CategoryDto(c.Id, c.Name))
            .ToList();
    }
}