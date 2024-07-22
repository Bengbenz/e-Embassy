// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

namespace Bengbenz.Embassy.eServices.UseCases.ServiceOffers.Create;

public class CreateServiceOfferRequest(string name, string description, string? imageUri, decimal unitPrice, bool featured, string createdBy, int? categoryId = null)
{
  public CreateServiceOfferRequest(): this(string.Empty, string.Empty, string.Empty, 0, false, string.Empty, null) { }
  public string Name { get; set; } = name;
  public string Description { get; set; } = description;
  public string? ImageUri { get; set; } = imageUri;
  public decimal UnitPrice { get; set; } = unitPrice;
  public bool Featured { get; set; } = featured;
  public string CreatedBy { get; set; } = createdBy;
  public int? CategoryId { get; set; } = categoryId;
  
}
