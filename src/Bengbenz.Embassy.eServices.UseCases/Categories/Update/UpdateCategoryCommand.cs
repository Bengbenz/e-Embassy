// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

using Ardalis.Result;
using Ardalis.SharedKernel;
using Bengbenz.Embassy.eServices.Core.CategoryAggregrate;

namespace Bengbenz.Embassy.eServices.UseCases.Categories.Update;

/// <summary>
/// Represents the command to update an existing category's properties.
/// </summary>
/// <param name="CategoryId">The unique identifier of the category to be updated.</param>
/// <param name="NewName">The new name to be assigned to the category.</param>
/// <remarks>
/// This command is part of the CQRS pattern, encapsulating the data needed solely for the update operation of a category's name.
/// It is intended to be handled by a corresponding command handler that executes the update process, ensuring the category's name is updated in the system.
/// </remarks>
public record UpdateCategoryCommand(int CategoryId, string NewName) : ICommand<Result<Category>>;
