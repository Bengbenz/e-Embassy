// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Bengbenz.Embassy.eServices.UseCases.Categories.List;

public record ListCategoriesQuery(int Skip = 0, int Take = 0) : IQuery<Result<IEnumerable<CategoryDto>>>;
