// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Bengbenz.Embassy.eServices.UseCases.Categories.List;

public record ListCategoriesQuery(int? Skip = null, int? Take = null) : IQuery<Result<IEnumerable<CategoryDto>>>;