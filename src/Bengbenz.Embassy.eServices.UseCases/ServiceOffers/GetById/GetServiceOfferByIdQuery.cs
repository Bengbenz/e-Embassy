// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

using Ardalis.Result;
using Ardalis.SharedKernel;
using Bengbenz.Embassy.eServices.Core.ServiceOfferAggregrate;

namespace Bengbenz.Embassy.eServices.UseCases.ServiceOffers.GetById;

public record GetServiceOfferByIdQuery(int ServiceId) : IQuery<Result<ServiceOffer>>;
