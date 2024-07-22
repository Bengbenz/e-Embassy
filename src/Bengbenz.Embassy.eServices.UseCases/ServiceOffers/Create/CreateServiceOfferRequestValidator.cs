// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

using Ardalis.SharedKernel;
using Bengbenz.Embassy.eServices.Core.ServiceOfferAggregrate;
using Bengbenz.Embassy.eServices.Core.ServiceOfferAggregrate.Specifications;
using FluentValidation;

namespace Bengbenz.Embassy.eServices.UseCases.ServiceOffers.Create;

public class CreateServiceOfferRequestValidator : BaseValidator<CreateServiceOfferRequest>
{
  private readonly IReadRepository<ServiceOffer> _repository;
  public CreateServiceOfferRequestValidator(IReadRepository<ServiceOffer> repository)
  {
    _repository = repository;
    
    RuleFor(x => x.Name)
      .NotEmpty()
      .WithMessage("Le nom du service est obligatoire !")
      .MinimumLength(2)
      .MaximumLength(100)
      .MustAsync(async (value, cancellationToken) => await IsUniqueAsync(value, cancellationToken))
      .WithMessage("Le nom du service doit être unique !");
    
     RuleFor(x => x.Description)
      .NotEmpty()
      .WithMessage("La description du service est obligatoire !")
      .MinimumLength(10)
      .MaximumLength(500);

    RuleFor(x => x.ImageUri)
      .Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute))
      .When(x => !string.IsNullOrEmpty(x.ImageUri))
      .WithMessage("L'URI de l'image doit être valide !");

    RuleFor(x => x.UnitPrice)
      .GreaterThan(0)
      .WithMessage("Le prix unitaire doit être supérieur à zéro !");

    RuleFor(x => x.Featured)
      .NotNull()
      .WithMessage("La propriété 'Featured' est obligatoire !");

    RuleFor(x => x.CreatedBy)
      .NotEmpty()
      .WithMessage("Le créateur du service est obligatoire !");

    RuleFor(x => x.CategoryId)
      .GreaterThan(0)
      .When(x => x.CategoryId.HasValue)
      .WithMessage("L'ID de la catégorie associée au service doit être supérieur à zéro !");
  
  }
  
  private async Task<bool> IsUniqueAsync(string name, CancellationToken token)
  { 
    var result = await _repository.AnyAsync(new ServiceOfferNameSpecification(name), token);
    return !result;
  }
}
