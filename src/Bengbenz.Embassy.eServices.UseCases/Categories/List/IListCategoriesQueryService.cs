// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

namespace Bengbenz.Embassy.eServices.UseCases.Categories.List;

public interface IListCategoriesQueryService
{
    Task<IEnumerable<CategoryDto>> ListAsync();
}