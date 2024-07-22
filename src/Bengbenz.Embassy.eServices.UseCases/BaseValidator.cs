// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

using FluentValidation;

namespace Bengbenz.Embassy.eServices.UseCases;

/// <summary>
/// Provides a base class for validators, extending the functionality of FluentValidation's AbstractValidator.
/// </summary>
/// <typeparam name="T">The type of the object being validated.</typeparam>
/// <remarks>
/// This base validator sets a custom rule level cascade mode to stop on the first failure.
/// It also provides a method to validate individual properties asynchronously, returning a collection of error messages if any.
/// </remarks>
public abstract class BaseValidator<T> : AbstractValidator<T>
{

  protected BaseValidator()
  {
    RuleLevelCascadeMode = CascadeMode.Stop;
  }
  public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
  {
    var result = await ValidateAsync(ValidationContext<T>.CreateWithOptions(
      (T)model, 
      x => x.IncludeProperties(propertyName)));
    
    return result.IsValid
      ? Array.Empty<string>()
      : result.Errors.Select(e => e.ErrorMessage);
  };
}
