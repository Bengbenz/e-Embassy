// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

using Bengbenz.Embassy.eServices.Core.CategoryAggregrate;
using Bengbenz.Embassy.eServices.Core.ServiceOfferAggregrate;

namespace Bengbenz.Embassy.eServices.UseCases.ServiceOffers.List;

public interface IListServiceOffersQueryService
{
  Task<IEnumerable<ServiceOffer>> GetServiceOffersAsync(int? skip = null, int? take = null, bool isFeatured = false);
}
