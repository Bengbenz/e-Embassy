// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

using Ardalis.Result;
using Ardalis.SharedKernel;
using Bengbenz.Embassy.eServices.Core.CategoryAggregrate;

namespace Bengbenz.Embassy.eServices.UseCases.Categories.Update;

/// <summary>
/// Handles the update operation for a category's properties.
/// </summary>
/// <param name="repository">The repository for accessing and manipulating category data.</param>
/// <param name="mapper">The AutoMapper instance for mapping domain entities to DTOs.</param>
/// <remarks>
/// This handler is responsible for updating the properties of an existing category, specifically its name in this case.
/// It first retrieves the category by its ID. If the category exists, it updates its name and persists the changes to the database.
/// Otherwise, it returns a NotFound result. Finally, it maps the updated category entity to a CategoryDto
/// and returns it as part of a successful operation result.
/// </remarks>
public sealed class UpdateCategoryHandler(IRepository<Category> repository)
  : ICommandHandler<UpdateCategoryCommand, Result<Category>>
{
  public async Task<Result<Category>> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
  {
    var existingEntity = await repository.GetByIdAsync(command.CategoryId, cancellationToken);
    if (existingEntity is null)
    {
      return Result.NotFound();
    }

    existingEntity.UpdateName(command.NewName);
    await repository.UpdateAsync(existingEntity, cancellationToken);

    return Result.Success(existingEntity);
  }
}
