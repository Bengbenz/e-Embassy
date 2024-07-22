// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Bengbenz.Embassy.eServices.UseCases.Categories.Update;

/// <summary>
/// Represents a request to update an existing category's details.
/// </summary>
/// <param name="id">The unique identifier of the category to be updated.</param>
/// <param name="name">The new name to be assigned to the category.</param>
/// <param name="parentCategoryId">Optional. The unique identifier of the parent category, if the category is to be a subcategory.</param>
/// <param name="parentCategoryName">Optional. The name of the parent category, if the category is to be a subcategory.</param>
/// <remarks>
/// This request model is used to encapsulate the data required for updating a category's information, including its name and optional parent category details.
/// The <see cref="ParentCategoryId"/> and <see cref="ParentCategoryName"/> properties are optional and used to specify if the updated category should be a subcategory.
/// </remarks>
public sealed class UpdateCategoryRequest(int id, string name, int? parentCategoryId = null, string? parentCategoryName = null)
{
  [Required]
  public int Id { get; set; } = id;

  [Required]
  public string Name { get; set; } = name;

  /// <summary>
  /// Gets or sets the optional parent category ID. If provided, the new category will be a subcategory of the specified parent.
  /// </summary>
  [JsonIgnore]
  public int? ParentCategoryId { get; set; } = parentCategoryId;
  
  /// <summary>
  /// Gets or sets the optional parent category name. If provided, the new category will be a subcategory of the specified parent.
  /// </summary>
  [JsonIgnore]
  public string? ParentCategoryName { get; set; } = parentCategoryName;
}
