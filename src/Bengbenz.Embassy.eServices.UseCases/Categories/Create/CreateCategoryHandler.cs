// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

using Ardalis.GuardClauses;
using Ardalis.Result;
using Ardalis.SharedKernel;
using Bengbenz.Embassy.eServices.Core.CategoryAggregrate;
using Bengbenz.Embassy.eServices.Core.CategoryAggregrate.Specifications;
using Bengbenz.Embassy.eServices.Core.Exceptions;

namespace Bengbenz.Embassy.eServices.UseCases.Categories.Create;

/// <summary>
/// Handles the creation of a new category.
/// </summary>
/// <param name="categoryRepository">The generic repository with write rights for accessing and storing categories.</param>
/// <remarks>
/// This handler is responsible for creating a new category in the system. It performs checks to ensure that
/// a category with the same name does not already exist to maintain uniqueness. Additionally, if a parent category ID is provided,
/// it verifies the existence of the parent category to ensure referential integrity. Otherwise it returns a NotFound result.
/// 
/// Exceptions:
/// - <see cref="DuplicateException"/>: Thrown if a category with the same name already exists.
/// </remarks>
public class CreateCategoryHandler(IRepository<Category> categoryRepository)
  : ICommandHandler<CreateCategoryCommand, Result<int>>
{

  /// <inheritdoc />
  public async Task<Result<int>> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
  {
    var categoryItemNameSpecification = new CategoryNameSpecification(command.Name);
    var existingCategoryItem = await categoryRepository.CountAsync(categoryItemNameSpecification, cancellationToken);
    if (existingCategoryItem > 0)
    {
      throw new DuplicateException($"A Category with name {command.Name} already exists.");
    }
    
    var newCategory = new Category(command.Name);
    if (command.ParentId.HasValue)
    {
      var spec = new CategoryByIdWithSubCategoriesSpec(command.ParentId.Value);
      var parentCategoryItem = await categoryRepository.FirstOrDefaultAsync(spec, cancellationToken);
      if (parentCategoryItem is null)
      {
        return Result.NotFound($"The parent category of {command.Name} is not found.");
      }
      parentCategoryItem.AddSubCategory(newCategory);
      await categoryRepository.UpdateAsync(parentCategoryItem, cancellationToken);
    }
    else
    {
      await categoryRepository.AddAsync(newCategory, cancellationToken);
    }
    
    return newCategory.Id;
  }
}
