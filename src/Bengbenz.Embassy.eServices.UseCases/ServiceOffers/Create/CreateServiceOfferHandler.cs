// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

using Ardalis.Result;
using Ardalis.SharedKernel;
using Bengbenz.Embassy.eServices.Core.CategoryAggregrate;
using Bengbenz.Embassy.eServices.Core.Exceptions;
using Bengbenz.Embassy.eServices.Core.ServiceOfferAggregrate;
using Bengbenz.Embassy.eServices.Core.ServiceOfferAggregrate.Specifications;

namespace Bengbenz.Embassy.eServices.UseCases.ServiceOffers.Create;

public class CreateServiceOfferHandler(
  IReadRepository<Category> categoryRepository,
  IRepository<ServiceOffer> serviceOfferRepository)
  : ICommandHandler<CreateServiceOfferCommand, Result<int>>
{
  /// <inheritdoc />
  public async Task<Result<int>> Handle(CreateServiceOfferCommand command, CancellationToken cancellationToken)
  {
    var serviceOfferItemNameSpecification = new ServiceOfferNameSpecification(command.Name);
    var existingServiceOfferItem = await serviceOfferRepository.CountAsync(serviceOfferItemNameSpecification, cancellationToken);
    if (existingServiceOfferItem > 0)
    {
      throw new DuplicateException($"A Service offer with name {command.Name} already exists.");
    }
    
    var newOffer = new ServiceOffer(
      command.Name,
      command.Description,
      command.UnitPrice,
      command.CreatedBy,
      command.ImageUri,
      command.IsFeatured);
    if (command.CategoryId.HasValue)
    {
      
      var categoryItem = await categoryRepository.GetByIdAsync(command.CategoryId!.Value, cancellationToken);
      if (categoryItem is null)
      {
        return Result.NotFound($"The category of {command.Name} is not found.");
      }
      newOffer.UpdateCategoryId(categoryItem.Id);
    }
    await serviceOfferRepository.AddAsync(newOffer, cancellationToken);
    
    return newOffer.Id;
  }
}
