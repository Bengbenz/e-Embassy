// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Bengbenz.Embassy.eServices.UseCases.ServiceOffers.Create;

public record CreateServiceOfferCommand(string Name, string Description, string? ImageUri, decimal UnitPrice, bool IsFeatured, string CreatedBy, int? CategoryId)
  : ICommand<Result<int>>;
