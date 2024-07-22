// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

using Ardalis.Result;
using Ardalis.SharedKernel;
using Bengbenz.Embassy.eServices.Core.ServiceOfferAggregrate;

namespace Bengbenz.Embassy.eServices.UseCases.ServiceOffers.Update;

public sealed class UpdateServiceOfferHandler(IRepository<ServiceOffer> repository)
  : ICommandHandler<UpdateServiceOfferCommand, Result<ServiceOffer>>
{
  public async Task<Result<ServiceOffer>> Handle(UpdateServiceOfferCommand command, CancellationToken cancellationToken)
  {
    var existingEntity = await repository.GetByIdAsync(command.Id, cancellationToken);
    if (existingEntity is null)
    {
      return Result.NotFound();
    }

    existingEntity.UpdateName(command.NewName);
    await repository.UpdateAsync(existingEntity, cancellationToken);

    return Result.Success(existingEntity);
  }
}
