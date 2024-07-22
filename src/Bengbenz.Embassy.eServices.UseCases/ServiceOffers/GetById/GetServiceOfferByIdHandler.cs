// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

using Ardalis.Result;
using Ardalis.SharedKernel;
using Bengbenz.Embassy.eServices.Core.ServiceOfferAggregrate;

namespace Bengbenz.Embassy.eServices.UseCases.ServiceOffers.GetById;

public sealed class GetServiceOfferByIdHandler(IReadRepository<ServiceOffer> repository)
  : IQueryHandler<GetServiceOfferByIdQuery, Result<ServiceOffer>>
{
  /// <inheritdoc />
  public async Task<Result<ServiceOffer>> Handle(GetServiceOfferByIdQuery request, CancellationToken cancellationToken)
  {
    var entity = await repository.GetByIdAsync(request.ServiceId, cancellationToken);
    if (entity is null) return Result.NotFound();

    return Result.Success(entity);
  }
}
