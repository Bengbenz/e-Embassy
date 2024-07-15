// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENCE] file.

using Ardalis.GuardClauses;
using Ardalis.SharedKernel;

namespace Bengbenz.Embassy.eServices.Core.CategoryAggregrate;

public class Category(string name) : EntityBase, IAggregateRoot
{
    public string Name { get; private set; } = Guard.Against.NullOrEmpty(name, nameof(name));
    
    private readonly List<Category> _subCategories = [];
    public IEnumerable<Category> SubCategories => _subCategories.AsReadOnly();
    
    public Category(string name, IEnumerable<Category> subCategories) : this(name)
    {
        _subCategories.AddRange(subCategories);
    }
    
    public Category UpdateName(string newName)
    {
        Name = Guard.Against.NullOrEmpty(newName, nameof(newName));
        return this;
    }
    
    public Category AddSubCategory(Category newSubCategory)
    {
        Guard.Against.Null(newSubCategory, nameof(newSubCategory));
        _subCategories.Add(newSubCategory);
        return this;
    }
}