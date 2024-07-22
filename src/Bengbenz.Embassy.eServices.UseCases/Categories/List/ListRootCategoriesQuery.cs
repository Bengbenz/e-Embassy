// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

using Ardalis.Result;
using Ardalis.SharedKernel;
using Bengbenz.Embassy.eServices.Core.CategoryAggregrate;

namespace Bengbenz.Embassy.eServices.UseCases.Categories.List;

/// <summary>
/// Represents a query for listing root categories with optional pagination.
/// </summary>
/// <param name="Skip">The number of categories to skip. Used for pagination.</param>
/// <param name="Take">The number of categories to take. Used for pagination.</param>
public record ListRootCategoriesQuery(int Skip = 0, int Take = 0) : IQuery<Result<IEnumerable<Category>>>;
