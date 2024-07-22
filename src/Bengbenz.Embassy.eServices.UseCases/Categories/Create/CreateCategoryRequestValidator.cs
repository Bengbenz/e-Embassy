// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

using FluentValidation;

namespace Bengbenz.Embassy.eServices.UseCases.Categories.Create;

/// <summary>
/// Defines the validator for creating a new category request.
/// </summary>
/// <remarks>
/// This validator extends the <see cref="BaseValidator{T}"/> to leverage common validation functionalities.
/// It includes rules to ensure the category name is not empty, meets length requirements, and is unique.
/// The uniqueness is verified through an asynchronous operation, simulating a long-running HTTP call.
/// </remarks>
public class CreateCategoryRequestValidator : BaseValidator<CreateCategoryRequest>
{
  public CreateCategoryRequestValidator()
  {
    RuleFor(x => x.Name)
      .NotEmpty()
      .WithMessage("Le nom de la catégorie est obligatoire !")
      .MinimumLength(2)
      .MaximumLength(100)
      .MustAsync(async (value, cancellationToken) => await IsUniqueAsync(value, cancellationToken));
  }
  
  private async Task<bool> IsUniqueAsync(string name, CancellationToken token)
  {
    // Simulates a long running http call
    await Task.Delay(2000);
    return name.ToLower() != "test";
  }
}
