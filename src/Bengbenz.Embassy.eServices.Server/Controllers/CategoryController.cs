// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

using Bengbenz.Embassy.eServices.UseCases.Categories;
using Bengbenz.Embassy.eServices.UseCases.Categories.List;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bengbenz.Embassy.eServices.Server.Controllers;

public sealed class CategoryController(ILogger<CategoryController> logger, IMediator mediator)
    : BaseApiController(logger, mediator)
{
    [HttpGet]
    public async Task<ActionResult<List<CategoryDto>>> List(CancellationToken cancellationToken)
    {
        var query = new ListCategoriesQuery();
        var result = await Mediator.Send(query, cancellationToken);
        return Ok(result);
    }
    
    // [HttpGet("{id}")]
    // public async Task<ActionResult<CategoryDto>> GetById(int id, CancellationToken cancellationToken)
    // {
    //     var query = new GetCategoryQuery(id);
    //     var result = await Mediator.Send(query, cancellationToken);
    //     return Ok(result);
    // }
    //
    // [HttpPost]
    // public async Task<ActionResult<int>> Create(CreateCategoryCommand command, CancellationToken cancellationToken)
    // {
    //     var result = await Mediator.Send(command, cancellationToken);
    //     return Ok(result);
    // }
    //
    // [HttpPut("{id}")]
    // public async Task<ActionResult> Update(int id, UpdateCategoryCommand command, CancellationToken cancellationToken)
    // {
    //     if (id != command.Id)
    //     {
    //         return BadRequest();
    //     }
    //     
    //     await Mediator.Send(command, cancellationToken);
    //     return NoContent();
    // }
    //
    // [HttpDelete("{id}")]
    // public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
    // {
    //     var command = new DeleteCategoryCommand(id);
    //     await Mediator.Send(command, cancellationToken);
    //     return NoContent();
    // }
}