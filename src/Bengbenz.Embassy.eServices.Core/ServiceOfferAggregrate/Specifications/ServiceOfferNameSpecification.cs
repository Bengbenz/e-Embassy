// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

using Ardalis.Specification;

namespace Bengbenz.Embassy.eServices.Core.ServiceOfferAggregrate.Specifications;

public sealed class ServiceOfferNameSpecification : Specification<ServiceOffer>
{
  public ServiceOfferNameSpecification(string name)
  {
    Query.Where(category => category.Name == name);
  }
}
