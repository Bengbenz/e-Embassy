// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Bengbenz.Embassy.eServices.UseCases.Categories.Create;

/// <summary>
/// Represents the command to create a new category.
/// </summary>
/// <param name="Name">The name of the category to be created. This parameter is required.</param>
/// <param name="ParentId">The optional ID of the parent category under which this new category will be nested.</param>
/// <remarks>
/// This command is used in the context of the CQRS pattern, encapsulating all the data needed to create a new category.
/// It is handled by a corresponding command handler which processes the creation logic, including validation and persistence.
/// </remarks>
public record CreateCategoryCommand(string Name, int? ParentId) : ICommand<Result<int>>;
