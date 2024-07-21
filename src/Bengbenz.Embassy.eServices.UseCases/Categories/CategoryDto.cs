// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

namespace Bengbenz.Embassy.eServices.UseCases.Categories;

public record CategoryDto(int Id, string Name, int? ParentCategoryId = null, string? ParentCategoryName = null)
{
  public string Name { get; set; } = Name;
  public string? ParentCategoryName { get; set; } = ParentCategoryName;
  public CategoryDto(): this(0, string.Empty)
  {
  }
  
  public IEnumerable<CategoryDto> SubCategories { get; } = [];
}
