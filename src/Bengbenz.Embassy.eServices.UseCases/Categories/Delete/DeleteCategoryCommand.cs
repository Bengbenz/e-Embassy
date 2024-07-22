// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Bengbenz.Embassy.eServices.UseCases.Categories.Delete;

/// <summary>
/// Represents the command to delete a specific category by its ID.
/// </summary>
/// <param name="CategoryId">The unique identifier of the category to be deleted.</param>
/// <remarks>
/// This command is part of the CQRS pattern, encapsulating the data needed solely for the deletion of a category.
/// It is intended to be handled by a corresponding command handler that executes the deletion process.
/// </remarks>
public record DeleteCategoryCommand(int CategoryId) : ICommand<Result>;
