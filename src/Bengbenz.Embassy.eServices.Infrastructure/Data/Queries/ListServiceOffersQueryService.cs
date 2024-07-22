// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

using Ardalis.SharedKernel;
using Bengbenz.Embassy.eServices.Core.CategoryAggregrate;
using Bengbenz.Embassy.eServices.Core.CategoryAggregrate.Specifications;
using Bengbenz.Embassy.eServices.Core.ServiceOfferAggregrate;
using Bengbenz.Embassy.eServices.Core.ServiceOfferAggregrate.Specifications;
using Bengbenz.Embassy.eServices.UseCases.Categories.List;
using Bengbenz.Embassy.eServices.UseCases.ServiceOffers.List;

namespace Bengbenz.Embassy.eServices.Infrastructure.Data.Queries;

public sealed class ListServiceOffersQueryService(IReadRepository<ServiceOffer> repository) : IListServiceOffersQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<ServiceOffer>> GetServiceOffersAsync(int? skip = null, int? take = null, bool isFeatured = false)
    {
      var spec = new ServiceOffersWithFeatureOptionSpec(isFeatured); 
      return await repository.ListAsync(spec);
    }
}
