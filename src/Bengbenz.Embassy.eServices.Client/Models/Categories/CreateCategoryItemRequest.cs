// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

using System.ComponentModel.DataAnnotations;

namespace Bengbenz.Embassy.eServices.Client.Models.Categories;

public class CreateCategoryItemRequest(string Name, int? ParentCategoryId = null)
{
  [Required(ErrorMessage = "Le nom de la catégorie est obligatoire field.")]
  public string Name { get; set; } = Name;
  public int? ParentCategoryId { get; set; } = ParentCategoryId;
}
