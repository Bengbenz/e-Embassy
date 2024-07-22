// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Bengbenz.Embassy.eServices.UseCases.Categories.Create;

/// <summary>
/// Represents the request to create a new category.
/// </summary>
/// <param name="name">The name of the category to be created. This field is required.</param>
/// <param name="parentCategoryId">The optional ID of the parent category under (for sub-category) which this category will be created.</param>
/// <param name="parentCategoryName">The optional name of the parent category under (for sub-category) which this category will be created.</param>

public class CreateCategoryRequest(string name = "", int? parentCategoryId = null, string? parentCategoryName = null)
{
  /// <summary>
  /// Gets or sets the name of the category. This field is required.
  /// </summary>
  [Required]
  public string Name { get; set; } = name;

  /// <summary>
  /// Gets or sets the optional parent category ID. If provided, the new category will be a subcategory of the specified parent.
  /// </summary>
  public int? ParentCategoryId { get; set; } = parentCategoryId;
  
  /// <summary>
  /// Gets or sets the optional parent category name. If provided, the new category will be a subcategory of the specified parent.
  /// </summary>
  [JsonIgnore]
  public string? ParentCategoryName { get; set; } = parentCategoryName;
}
