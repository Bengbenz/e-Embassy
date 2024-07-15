// MIT License.
// \Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

using Bengbenz.Embassy.eServices.Core.CategoryAggregrate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bengbenz.Embassy.eServices.Infrastructure.Data.Config;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(b => b.Name)
            .IsRequired()
            .HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH);

        builder.HasMany(c => c.SubCategories)
            .WithOne() // Assuming SubCategory does not have a navigation property back to Category
            .HasForeignKey("ParentCategoryId") // Assuming there's a shadow ParentCategoryId in the SubCategory entity
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);
    }
}