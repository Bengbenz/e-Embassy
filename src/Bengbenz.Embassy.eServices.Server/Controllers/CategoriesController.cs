// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

using Ardalis.Result;
using AutoMapper;
using Bengbenz.Embassy.eServices.Core.CategoryAggregrate;
using Bengbenz.Embassy.eServices.UseCases.Categories;
using Bengbenz.Embassy.eServices.UseCases.Categories.Create;
using Bengbenz.Embassy.eServices.UseCases.Categories.Delete;
using Bengbenz.Embassy.eServices.UseCases.Categories.GetWithSubCategories;
using Bengbenz.Embassy.eServices.UseCases.Categories.List;
using Bengbenz.Embassy.eServices.UseCases.Categories.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bengbenz.Embassy.eServices.Server.Controllers;

/// <summary>
/// Controller responsible for handling category-related requests.
/// </summary>
/// <remarks>
/// This controller provides endpoints for listing categories, retrieving a specific category by ID,
/// creating new categories, updating existing categories and deleting categories. It leverages MediatR for command and query handling, ensuring a clean separation
/// of concerns and adherence to the CQRS pattern. AutoMapper is used to map between domain entities and DTOs,
/// simplifying the transformation logic.
/// </remarks>
public sealed class CategoriesController(ILogger<CategoriesController> logger, IMediator mediator, IMapper mapper)
  : BaseApiController(logger, mediator)
{
  /// <summary>
  /// Lists all root categories along with their subcategories.
  /// </summary>
  /// <param name="cancellationToken">Cancellation token to cancel the request.</param>
  /// <returns>A list of root categories with their subcategories.</returns>
  /// <response code="200">Returns the list of categories</response>
  /// <response code="404">If no categories are found</response>
  [HttpGet]
  [ProducesResponseType(typeof(List<CategoryDto>), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status200OK)]
  public async Task<ActionResult<List<CategoryDto>>> ListAsync(CancellationToken cancellationToken)
  {
    var query = new ListRootCategoriesQuery();
    Result<IEnumerable<Category>> result = await Mediator.Send(query, cancellationToken);
    if (result.IsSuccess)
    {
      var categories = new List<CategoryDto>();
      foreach (var c in result.Value)
      {
        var categoryDto = mapper.Map<CategoryDto>(c);
        categoryDto.SubCategories = c.SubCategories.Select(s => new CategoryDto(s.Id, s.Name, s.ParentCategoryId, c.Name));;
        categories.Add(categoryDto);
      }
      return Ok(categories);
    }

    return NotFound();
  }

  /// <summary>
  /// Retrieves a category by its ID along with all its subcategories.
  /// </summary>
  /// <param name="id">The ID of the category to retrieve.</param>
  /// <param name="cancellationToken">Cancellation token to cancel the request.</param>
  /// <returns>The requested category with its subcategories.</returns>
  /// <response code="200">Returns the requested category and its subcategories</response>
  /// <response code="404">If the category is not found</response>
  [HttpGet("{id}")]
  [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status200OK)]
  public async Task<ActionResult<CategoryDto>> GetByIdAsync([FromRoute] int id, CancellationToken cancellationToken)
  {
    var query = new GetCategoryWithAllSubCategoriesQuery(id);
    Result<Category> result = await Mediator.Send(query, cancellationToken);

    if (result.IsSuccess)
    {
      var categoryDto = mapper.Map<CategoryDto>(result.Value);
      var subCategories = result.Value.SubCategories.ToList();
      categoryDto.SubCategories = subCategories.Select(c => new CategoryDto(c.Id, c.Name, c.ParentCategoryId, categoryDto.Name));
      
      logger.LogInformation($"Categorie Id: {categoryDto.Id},  Name: {categoryDto.Name}, SubCategories: ({subCategories.Count})");
      return Ok(categoryDto);
    }
    
    return NotFound();
  }

  /// <summary>
  /// Creates a new category.
  /// </summary>
  /// <param name="request">The request to create a new category.</param>
  /// <param name="cancellationToken">Cancellation token to cancel the request.</param>
  /// <returns>The ID of the newly created category.</returns>
  /// <response code="200">Returns the ID of the newly created category</response>
  /// <response code="204">If the request is successful but no content is created</response>
  [HttpPost]
  [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  public async Task<ActionResult<int>> CreateAsync([FromBody] CreateCategoryRequest request, CancellationToken cancellationToken)
  {
      Result<int> result = await Mediator.Send(new CreateCategoryCommand(request.Name, request.ParentCategoryId), cancellationToken);
      if (result.IsSuccess)
      {
        return Ok(result);
      }

      return NoContent();
  }

  /// <summary>
  /// Updates an existing category with the provided details.
  /// </summary>
  /// <param name="request">The request containing the new details for the category.</param>
  /// <param name="cancellationToken">A token to cancel the operation if necessary.</param>
  /// <returns>An action result containing the updated category DTO if successful, NotFound if the category does not exist, or NoContent if the update operation did not result in any changes.</returns>
  /// <remarks>
  /// This method handles HTTP PUT requests to update category details. It sends an UpdateCategoryCommand to the Mediator,
  /// checks the result status, and returns the appropriate action result based on the operation outcome.
  /// </remarks>
  /// <response code="200">Returns the updated category DTO</response>
  /// <response code="404">If the category does not exist</response>
  /// <response code="204">If the update operation did not result in any changes</response>
  [HttpPut]
  [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  public async Task<ActionResult<CategoryDto>> UpdateAsync([FromBody]UpdateCategoryRequest request, CancellationToken cancellationToken)
  {
      var command = new UpdateCategoryCommand(request.Id, request.Name);
      Result<Category> result = await Mediator.Send(command, cancellationToken);

      if (result.Status == ResultStatus.NotFound)
      {
        return NotFound();
      }
      if (result.IsSuccess)
      {
        return Ok(mapper.Map<CategoryDto>(result.Value));
      }

      return NoContent();
  }
    
  /// <summary>
  /// Deletes a category by its unique identifier.
  /// </summary>
  /// <param name="id">The unique identifier of the category to be deleted.</param>
  /// <param name="cancellationToken">A token to cancel the operation if necessary.</param>
  /// <returns>An action result indicating the outcome of the delete operation. Returns NotFound if the category does not exist, or NoContent if the deletion was successful.</returns>
  /// <remarks>
  /// This method handles HTTP DELETE requests to remove a category from the system. It sends a DeleteCategoryCommand to the Mediator,
  /// checks the result status, and returns the appropriate action result based on whether the category was found and successfully deleted.
  /// </remarks>
  /// <response code="404">If the category does not exist</response>
  /// <response code="204">If the deletion was successful</response>
  [HttpDelete("{id}")]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  public async Task<ActionResult> DeleteAsync([FromRoute]int id, CancellationToken cancellationToken)
  {
      var command = new DeleteCategoryCommand(id);
      Result result = await Mediator.Send(command, cancellationToken);

      if (result.Status == ResultStatus.NotFound)
      {
        Result.NotFound();
      }
      
      return NoContent();
  }
}
