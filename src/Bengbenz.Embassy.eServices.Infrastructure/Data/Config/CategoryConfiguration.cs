// MIT License.
// \Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

using Bengbenz.Embassy.eServices.Core.CategoryAggregrate;
using Bengbenz.Embassy.eServices.Core.ServiceOfferAggregrate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bengbenz.Embassy.eServices.Infrastructure.Data.Config;

public sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(b => b.Id)
            .IsRequired()
            .UseHiLo("category_hilo");
        
        builder.Property(b => b.Name)
            .IsRequired()
            .HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH);
        
        builder.HasIndex(b => b.Name)
          .IsUnique(); // Add unique constraint on Name

        builder.HasMany(c => c.SubCategories)
            .WithOne() // Assuming SubCategory does not have a navigation property back to Category
            .HasForeignKey(s => s.ParentCategoryId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany<ServiceOffer>()
            .WithOne() // Assuming ServiceOffer has a navigation property back to Category
            .HasForeignKey(s => s.CategoryId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
