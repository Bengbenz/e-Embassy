// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Bengbenz.Embassy.eServices.UseCases.ServiceOffers.Update;

public sealed class UpdateServiceOfferRequest(int id, string name, string description, int? categoryId)
{
  public UpdateServiceOfferRequest(): this(0, string.Empty, string.Empty, null) { }
  public int Id { get; set; } = id;
  public string Name { get; set; } = name;
  public string Description { get; set; } = description;
  
  public int? CategoryId { get; set; } = categoryId;
}
