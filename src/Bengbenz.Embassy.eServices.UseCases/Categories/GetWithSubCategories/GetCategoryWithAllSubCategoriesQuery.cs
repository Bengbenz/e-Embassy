// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

using Ardalis.Result;
using Ardalis.SharedKernel;
using Bengbenz.Embassy.eServices.Core.CategoryAggregrate;

namespace Bengbenz.Embassy.eServices.UseCases.Categories.GetWithSubCategories;

/// <summary>
/// Represents a query to retrieve a category, based on the category's identifier, along with all its subcategories.
/// </summary>
/// <param name="CategoryId">The unique identifier of the category to retrieve.</param>
public record GetCategoryWithAllSubCategoriesQuery(int CategoryId) : IQuery<Result<Category>>;
