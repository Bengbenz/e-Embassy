// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENCE] file.

using Ardalis.GuardClauses;
using Ardalis.SharedKernel;
using Bengbenz.Embassy.eServices.Core.CategoryAggregrate;

namespace Bengbenz.Embassy.eServices.Core.ServiceOfferAggregrate;

public class ServiceOffer(
  string name,
  string description,
  decimal unitPrice,
  string createdBy,
  string? imageUri = default,
  bool isFeatured = false,
  int? categoryId = default) : EntityBase, IAggregateRoot
{
  public string Name { get; private set; } = Guard.Against.NullOrEmpty(name, nameof(name));
  public string Description { get; private set; } = Guard.Against.NullOrEmpty(description, nameof(description));

  public string? ImageUri { get; private set; } = imageUri; // image in base64
  // public PaymentType AcceptedPaymentType { get; private set; } TODO: Add payment type later
  
  public decimal UnitPrice { get; private set; } = Guard.Against.NegativeOrZero(unitPrice, nameof(unitPrice));
  // public string? Currency { get; private set; } TODO: Add currency later
  public bool IsFeatured { get; private set; } = isFeatured;
  public bool IsPublished { get; private set; }
  public string CreatedBy { get; private set; } = Guard.Against.NullOrEmpty(createdBy, nameof(createdBy)); // user id
  public DateTime CreatedAt { get; private set; } = DateTime.Now;
  public DateTime UpdatedAt { get; private set; } = DateTime.Now;
  public DateTime? PublishedAt { get; private set; }
  public int? CategoryId { get; private set; } =  categoryId;
  public Category? Category { get; private set; }

  public ServiceOffer UpdateName(string newName)
  {
      Name = Guard.Against.NullOrEmpty(newName, nameof(newName));
      return this;
  }
  
  public ServiceOffer UpdateDescription(string newDescription)
  {
      Description = Guard.Against.NullOrEmpty(newDescription, nameof(newDescription));
      return this;
  }
  
  public ServiceOffer UpdateImageUri(string newImageUri)
  {
      if (string.IsNullOrEmpty(newImageUri))
      {
        ImageUri = string.Empty;
      }
      ImageUri = $"img\\service-offers\\{newImageUri}?{DateTime.Now.Ticks}";
      return this;
  }
  
  public ServiceOffer UpdateUnitPrice(decimal newPrice)
  {
      UnitPrice = Guard.Against.NegativeOrZero(newPrice, nameof(newPrice));
      return this;
  }
  
  public ServiceOffer UpdateDetails(ServiceOfferDetails details)
  {
    Guard.Against.Null(details, nameof(details));
    return UpdateName(details.Name!)
      .UpdateDescription(details.Description!)
      .UpdateUnitPrice(details.Price);
  }
  
  public ServiceOffer UpdateIsFeatured(bool isFeatured)
  {
    IsFeatured = isFeatured;
    return this;
  }
  
  public ServiceOffer UpdatePublished(bool isPublished)
  {
    IsPublished = isPublished;
    return this;
  }
  
  public ServiceOffer UpdateCreatedBy(string userId)
  {
    CreatedBy = Guard.Against.NullOrEmpty(userId, nameof(userId));
    return this;
  }
  
  public ServiceOffer UpdateCreatedAt(DateTime newCreatedAt)
  {
      CreatedAt = Guard.Against.OutOfSQLDateRange(newCreatedAt, nameof(newCreatedAt), "Creation date cannot be in the past.");
      return this;
  }
  
  public ServiceOffer UpdateUpdatedAt(DateTime newUpdatedAt)
  {
      UpdatedAt = Guard.Against.OutOfSQLDateRange(newUpdatedAt, nameof(newUpdatedAt), "Update date cannot be in the past.");
      return this;
  }
  
  public ServiceOffer UpdatePublishedAt(DateTime publishedAt)
  {
    PublishedAt = Guard.Against.OutOfSQLDateRange(publishedAt, nameof(publishedAt), "Published date cannot be in the past.");
    return this;
  }
  
  public ServiceOffer UpdateCategoryId(int categoryId)
  {
    CategoryId = Guard.Against.Zero(categoryId, nameof(categoryId));
    return this;
  }
}
