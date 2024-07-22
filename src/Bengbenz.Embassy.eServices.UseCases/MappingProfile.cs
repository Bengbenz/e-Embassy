// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

using AutoMapper;
using Bengbenz.Embassy.eServices.Core.CategoryAggregrate;
using Bengbenz.Embassy.eServices.Core.ServiceOfferAggregrate;
using Bengbenz.Embassy.eServices.UseCases.Categories;
using Bengbenz.Embassy.eServices.UseCases.ServiceOffers;

namespace Bengbenz.Embassy.eServices.UseCases;

public class MappingProfile : Profile
{
  public MappingProfile()
  {
    CreateMap<Category, CategoryDto>();
    CreateMap<ServiceOffer, ServiceOfferDto>();
  }
}
