// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

using Ardalis.Result;
using Ardalis.SharedKernel;
using Bengbenz.Embassy.eServices.Core.CategoryAggregrate;
using Bengbenz.Embassy.eServices.Core.ServiceOfferAggregrate;

namespace Bengbenz.Embassy.eServices.UseCases.ServiceOffers.List;

public record ListServiceOffersQuery(int Skip = 0, int Take = 0, bool IsFeatured = false) : IQuery<Result<IEnumerable<ServiceOffer>>>;
