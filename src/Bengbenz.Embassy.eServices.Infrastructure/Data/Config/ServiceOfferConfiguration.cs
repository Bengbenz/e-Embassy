// MIT License.
// \Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

using Bengbenz.Embassy.eServices.Core.ServiceOfferAggregrate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bengbenz.Embassy.eServices.Infrastructure.Data.Config;

public sealed class ServiceOfferConfiguration : IEntityTypeConfiguration<ServiceOffer>
{
    public void Configure(EntityTypeBuilder<ServiceOffer> builder)
    {
      builder.Property(b => b.Id)
        .IsRequired()
        .UseHiLo("service_offer_hilo");
      
        builder.Property(b => b.Name)
            .IsRequired()
            .HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH);
        
        builder.HasIndex(b => b.Name)
          .IsUnique(); // Add unique constraint on Name
        
        builder.Property(b => b.Description)
          .IsRequired()
          .HasMaxLength(DataSchemaConstants.DEFAULT_DESCRIPTION_LENGTH);

        builder.Property(b => b.ImageUri)
          .IsRequired(false);

        builder.Property(b => b.UnitPrice)
          .IsRequired()
          .HasColumnType("decimal(18,2)");

        builder.Property(b => b.IsFeatured)
          .IsRequired();

        builder.Property(b => b.CreatedBy)
          .IsRequired();
        
        builder.Property(b => b.IsPublished)
          .IsRequired();

        builder.Property(b => b.CreatedAt)
          .IsRequired()
          .HasColumnType("timestamp without time zone")
          .ValueGeneratedOnAdd();

        builder.Property(b => b.UpdatedAt)
          .IsRequired()
          .HasColumnType("timestamp without time zone")
          .ValueGeneratedOnAddOrUpdate()
          .HasDefaultValueSql("CURRENT_TIMESTAMP"); // Set default value to current timestamp;

        builder.Property(b => b.PublishedAt)
          .IsRequired(false)
          .HasColumnType("timestamp without time zone");

        builder.HasOne(b => b.Category)
          .WithMany() 
          .HasForeignKey(b => b.CategoryId)
          .IsRequired(false);
    }
}
