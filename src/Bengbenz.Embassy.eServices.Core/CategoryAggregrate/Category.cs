// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENCE] file.

using Ardalis.GuardClauses;
using Ardalis.SharedKernel;

namespace Bengbenz.Embassy.eServices.Core.CategoryAggregrate;

/// <summary>
/// Represents a category entity with properties for name, subcategories, and an optional parent category ID.
/// </summary>
/// <remarks>
/// This class extends EntityBase and implements IAggregateRoot, making it a key part of the domain model.
/// It includes methods for updating the category name, adding subcategories, and managing its properties.
/// </remarks>
public class Category(string name) : EntityBase, IAggregateRoot
{
    /// <summary>
    /// The name of the category. Cannot be null or empty.
    /// </summary>
    public string Name { get; private set; } = Guard.Against.NullOrEmpty(name, nameof(name));
    
    /// <summary>
    /// A list of subcategories belonging to this category.
    /// </summary>
    private readonly List<Category> _subCategories = [];
    
    /// <summary>
    /// Provides read-only access to the subcategories.
    /// </summary>
    public IEnumerable<Category> SubCategories => _subCategories.AsReadOnly();
    
    /// <summary>
    /// The optional unique identifier of the parent category.
    /// </summary>
    public int? ParentCategoryId { get; private set; }
    
    /// <summary>
    /// Constructs a new Category instance with specified name and subcategories.
    /// </summary>
    /// <param name="name">The name of the category.</param>
    /// <param name="subCategories">A collection of subcategories.</param>
    public Category(string name, IEnumerable<Category> subCategories) : this(name)
    {
        _subCategories.AddRange(subCategories);
    }
    
    /// <summary>
    /// Updates the name of the category.
    /// </summary>
    /// <param name="newName">The new name for the category.</param>
    /// <returns>The updated category.</returns>
    public Category UpdateName(string newName)
    {
        Name = Guard.Against.NullOrEmpty(newName, nameof(newName));
        return this;
    }
    
    /// <summary>
    /// Adds a new subcategory to this category.
    /// </summary>
    /// <param name="newSubCategory">The subcategory to add.</param>
    /// <returns>The category with the new subcategory added.</returns>
    public Category AddSubCategory(Category newSubCategory)
    {
        Guard.Against.Null(newSubCategory, nameof(newSubCategory));
        _subCategories.Add(newSubCategory);
        return this;
    }
}
