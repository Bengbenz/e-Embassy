// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

using FluentValidation;

namespace Bengbenz.Embassy.eServices.UseCases.Categories.Update;

/// <summary>
/// Validates the request to update an existing category's details.
/// </summary>
/// <remarks>
/// Inherits from <see cref="BaseValidator{T}"/> to utilize common validation functionalities.
/// Defines rules to ensure the ID is greater than 0, the name is not empty, and the name's length is within the specified range.
/// </remarks>
public class UpdateCategoryRequestValidator : BaseValidator<UpdateCategoryRequest>
{
  public UpdateCategoryRequestValidator()
  {
    RuleFor(request => request.Id)
      .GreaterThan(0);

    RuleFor(request => request.Name)
      .NotEmpty()
      .MinimumLength(2)
      .MaximumLength(100);
  }
}
