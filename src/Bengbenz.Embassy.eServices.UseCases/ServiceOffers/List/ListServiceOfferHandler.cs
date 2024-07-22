// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

using Ardalis.Result;
using Ardalis.SharedKernel;
using Bengbenz.Embassy.eServices.Core.ServiceOfferAggregrate;

namespace Bengbenz.Embassy.eServices.UseCases.ServiceOffers.List;


public sealed class ListServiceOfferHandler(IListServiceOffersQueryService queryService)
    : IQueryHandler<ListServiceOffersQuery, Result<IEnumerable<ServiceOffer>>>
{
     /// <inheritdoc />
    public async  Task<Result<IEnumerable<ServiceOffer>>> Handle(ListServiceOffersQuery query, CancellationToken cancellationToken)
    {
        var items = await queryService.GetServiceOffersAsync();
        
        return Result.Success(items);
    }
}
