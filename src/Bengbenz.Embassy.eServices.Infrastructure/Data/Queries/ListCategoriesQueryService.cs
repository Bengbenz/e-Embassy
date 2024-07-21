// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

using Ardalis.SharedKernel;
using Bengbenz.Embassy.eServices.Core.CategoryAggregrate;
using Bengbenz.Embassy.eServices.Core.CategoryAggregrate.Specifications;
using Bengbenz.Embassy.eServices.UseCases.Categories.List;

namespace Bengbenz.Embassy.eServices.Infrastructure.Data.Queries;

public sealed class ListCategoriesQueryService(IReadRepository<Category> repository) : IListCategoriesQueryService
{
    public async Task<IEnumerable<Category>> ListAsync()
    {
        var categories = await repository.ListAsync();
        return categories.ToList();
    }
    
    public async Task<IEnumerable<Category>> GetCategoriesWithSubCategoriesAsync()
    {
      var spec = new CategoryListWithSubCategoriesSpec(); 
      return await repository.ListAsync(spec);
    }
}
