// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

namespace Bengbenz.Embassy.eServices.UseCases.ServiceOffers;

public record ServiceOfferDto(int Id, string Name, string Description, decimal UnitPrice, string CreatedBy)
{
  public ServiceOfferDto():this(0, string.Empty, string.Empty, default,string.Empty)
  {}
  public int Id { get; } = Id;
  public string Name { get; set; } = Name;
  public string Description { get; set; } = Description;
  public string? ImageUri { get; set; }
  public decimal UnitPrice { get; set; } = UnitPrice;
  public bool IsFeatured { get; set; }
  public bool IsPublished { get; set; }
  public string CreatedBy { get; set; } = CreatedBy;
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
  public DateTime? PublishedAt { get; set; }
  public int? CategoryId { get; set; }
}
