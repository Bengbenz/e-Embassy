// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

using Ardalis.Result;
using AutoMapper;
using Bengbenz.Embassy.eServices.Core.ServiceOfferAggregrate;
using Bengbenz.Embassy.eServices.UseCases.Categories;
using Bengbenz.Embassy.eServices.UseCases.ServiceOffers;
using Bengbenz.Embassy.eServices.UseCases.ServiceOffers.Create;
using Bengbenz.Embassy.eServices.UseCases.ServiceOffers.Delete;
using Bengbenz.Embassy.eServices.UseCases.ServiceOffers.GetById;
using Bengbenz.Embassy.eServices.UseCases.ServiceOffers.List;
using Bengbenz.Embassy.eServices.UseCases.ServiceOffers.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bengbenz.Embassy.eServices.Server.Controllers;

public sealed class ServiceOffersController(ILogger<ServiceOffersController> logger, IMediator mediator, IMapper mapper)
  : BaseApiController(logger, mediator)
{

  [HttpGet]
  [ProducesResponseType(typeof(List<ServiceOfferDto>), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status200OK)]
  public async Task<ActionResult<List<ServiceOfferDto>>> ListAsync(CancellationToken cancellationToken)
  {
    var query = new ListServiceOffersQuery();
    Result<IEnumerable<ServiceOffer>> result = await Mediator.Send(query, cancellationToken);
    if (result.IsSuccess)
    {
      var offers = result.Value.Select(mapper.Map<ServiceOfferDto>).ToList();
      return Ok(offers);
    }

    return NotFound();
  }
  
  [HttpGet("{id}")]
  [ProducesResponseType(typeof(ServiceOfferDto), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status200OK)]
  public async Task<ActionResult<ServiceOfferDto>> GetByIdAsync([FromRoute] int id, CancellationToken cancellationToken)
  {
    var query = new GetServiceOfferByIdQuery(id);
    Result<ServiceOffer> result = await Mediator.Send(query, cancellationToken);

    if (result.IsSuccess)
    {
      var serviceOfferDto = mapper.Map<ServiceOfferDto>(result.Value);
      // var subCategories = result.Value.SubCategories.ToList();
      // categoryDto.SubCategories = subCategories.Select(c => new CategoryDto(c.Id, c.Name, c.ParentCategoryId, categoryDto.Name));
      //
      // logger.LogInformation($"Categorie Id: {categoryDto.Id},  Name: {categoryDto.Name}, SubCategories: ({subCategories.Count})");
      return Ok(serviceOfferDto);
    }
    
    return NotFound();
  }


  [HttpPost]
  [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  public async Task<ActionResult<int>> CreateAsync([FromBody] CreateServiceOfferRequest request, CancellationToken cancellationToken)
  {
    var command = new CreateServiceOfferCommand(
      request.Name,
      request.Description,
      request.ImageUri,
      request.UnitPrice,
      request.Featured,
      request.CreatedBy,
      request.CategoryId);
    
      Result<int> result = await Mediator.Send(command, cancellationToken);
      if (result.IsSuccess)
      {
        return Ok(result);
      }

      return NoContent();
  }

  [HttpPut]
  [ProducesResponseType(typeof(ServiceOfferDto), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  public async Task<ActionResult<ServiceOfferDto>> UpdateAsync([FromBody]UpdateServiceOfferRequest request, CancellationToken cancellationToken)
  {
      var command = new UpdateServiceOfferCommand(
        request.Id,
        request.Name);
      Result<ServiceOffer> result = await Mediator.Send(command, cancellationToken);

      if (result.Status == ResultStatus.NotFound)
      {
        return NotFound();
      }
      if (result.IsSuccess)
      {
        return Ok(mapper.Map<ServiceOfferDto>(result.Value));
      }

      return NoContent();
  }
  
  [HttpDelete("{id}")]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  public async Task<ActionResult> DeleteAsync([FromRoute]int id, CancellationToken cancellationToken)
  {
      var command = new DeleteServiceOfferCommand(id);
      Result result = await Mediator.Send(command, cancellationToken);

      if (result.Status == ResultStatus.NotFound)
      {
        Result.NotFound();
      }
      
      return NoContent();
  }
}
