// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

namespace Bengbenz.Embassy.eServices.UseCases.Categories;

/// <summary>
/// Represents a data transfer object for a category.
/// </summary>
/// <param name="Id">The unique identifier of the category.</param>
/// <param name="Name">The name of the category. This field is required.</param>
/// <param name="ParentCategoryId">The optional unique identifier of the parent category, if any.</param>
/// <param name="ParentCategoryName">The optional name of the parent category, if any.</param>
/// <remarks>
/// This record is used to transfer category data between processes or layers, encapsulating the category's identity,
/// its name, and optionally, its relationship to a parent category. It includes validation attributes to ensure data integrity.
/// </remarks>
public record CategoryDto(int Id, string Name, int? ParentCategoryId = null, string? ParentCategoryName = null)
{
  public string Name { get; set; } = Name;
  public string? ParentCategoryName { get; set; } = ParentCategoryName;
  public CategoryDto(): this(0, string.Empty)
  {
  }
  
  public IEnumerable<CategoryDto> SubCategories { get; set; } = [];
}
