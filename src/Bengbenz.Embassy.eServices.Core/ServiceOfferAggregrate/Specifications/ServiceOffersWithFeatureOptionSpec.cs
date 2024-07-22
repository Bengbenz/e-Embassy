// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

using Ardalis.Specification;
using Bengbenz.Embassy.eServices.Core.CategoryAggregrate;

namespace Bengbenz.Embassy.eServices.Core.ServiceOfferAggregrate.Specifications;

/// <summary>
/// Defines a specification for retrieving categories that do not have a parent category (root categories)
/// and includes their subcategories in the result.
/// </summary>
/// <remarks>
/// This specification is useful for scenarios where a hierarchical view of categories is needed,
/// starting from the root categories and including their immediate children.
/// </remarks>
public sealed class ServiceOffersWithFeatureOptionSpec : Specification<ServiceOffer>
{
  /// <summary>
  /// Initializes a new instance of the <see cref="ServiceOffersWithFeatureOptionSpec"/> class.
  /// </summary>
  public ServiceOffersWithFeatureOptionSpec(bool isFeatured)
  {
    Query
      .Where(c => c.IsFeatured == isFeatured);
  }
}
