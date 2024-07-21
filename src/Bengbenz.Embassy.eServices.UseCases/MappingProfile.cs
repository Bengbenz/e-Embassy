// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

using AutoMapper;
using Bengbenz.Embassy.eServices.Core.CategoryAggregrate;
using Bengbenz.Embassy.eServices.UseCases.Categories;

namespace Bengbenz.Embassy.eServices.UseCases;

public class MappingProfile : Profile
{
  public MappingProfile()
  {
    CreateMap<Category, CategoryDto>();
    // .ForMember(dest => dest.SubCategories, opt => opt.MapFrom(src => src.SubCategories));
  }
}
