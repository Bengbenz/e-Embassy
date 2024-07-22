// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

using Ardalis.SharedKernel;

namespace Bengbenz.Embassy.eServices.Core.ServiceOfferAggregrate;

public abstract class BaseServiceTerms : EntityBase, IAggregateRoot
{
  public string? StepsDescription { get; private set; }
  public string? ProvidedDocumentsDescription { get; private set; }
  public string? PriceDescription { get; private set; }
  public string? DelayDescription { get; private set; }
  public string? PaymentDescription { get; private set; }
  public string? RefundDescription { get; private set; }
  public string? DeliveryDescription { get; private set; }
}
