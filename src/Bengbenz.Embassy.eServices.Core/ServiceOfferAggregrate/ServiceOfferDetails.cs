// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

namespace Bengbenz.Embassy.eServices.Core.ServiceOfferAggregrate;

public readonly record struct ServiceOfferDetails(string? Name, string? Description, decimal Price)
{
  public string? Name { get; } = Name;
  public string? Description { get; } = Description;
  public decimal Price { get; } = Price;
}
