// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

using Bengbenz.Embassy.eServices.Core.CategoryAggregrate;

namespace Bengbenz.Embassy.eServices.UseCases.Categories.List;

public interface IListCategoriesQueryService
{
    Task<IEnumerable<Category>> ListAsync();

    Task<IEnumerable<Category>> GetCategoriesWithSubCategoriesAsync();
}
