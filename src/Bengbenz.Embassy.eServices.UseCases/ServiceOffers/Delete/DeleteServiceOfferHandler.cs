// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

using Ardalis.Result;
using Ardalis.SharedKernel;
using Bengbenz.Embassy.eServices.Core.CategoryAggregrate;

namespace Bengbenz.Embassy.eServices.UseCases.ServiceOffers.Delete;

/// <summary>
/// Handles the deletion of a category.
/// </summary>
/// <param name="repository">The repository for accessing and manipulating category data.</param>
/// <remarks>
/// This handler is responsible for deleting a specified category from the system. It first attempts to retrieve the category
/// by its ID. If the category is found, it proceeds with the deletion; otherwise, it returns a NotFound result.
/// </remarks>
public sealed class DeleteServiceOfferHandler(IRepository<Category> repository)
  : ICommandHandler<DeleteServiceOfferCommand, Result>
{
  /// <inheritdoc />
  public async Task<Result> Handle(DeleteServiceOfferCommand command, CancellationToken cancellationToken)
  {
    var aggregateToDelete = await repository.GetByIdAsync(command.CategoryId, cancellationToken);
    if (aggregateToDelete is null)
    {
      return Result.NotFound();
    }

    await repository.DeleteAsync(aggregateToDelete, cancellationToken);
    return Result.Success();
  }
}
