// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

using Ardalis.SharedKernel;
using Bengbenz.Embassy.eServices.Core.CategoryAggregrate;
using Bengbenz.Embassy.eServices.Core.CategoryAggregrate.Specifications;
using Bengbenz.Embassy.eServices.UseCases.Categories.List;

namespace Bengbenz.Embassy.eServices.Infrastructure.Data.Queries;

/// <summary>
/// Provides implementation for querying categories and their subcategories from a repository.
/// </summary>
/// <param name="repository">The repository to access category data.</param>
public sealed class ListCategoriesQueryService(IReadRepository<Category> repository) : IListCategoriesQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<Category>> ListAsync()
    {
      return await repository.ListAsync();
    }
    
    /// <inheritdoc />
    public async Task<IEnumerable<Category>> GetRootCategoriesWithSubCategoriesAsync()
    {
      var spec = new RootCategoryListWithSubCategoriesSpec(); 
      return await repository.ListAsync(spec);
    }
}
