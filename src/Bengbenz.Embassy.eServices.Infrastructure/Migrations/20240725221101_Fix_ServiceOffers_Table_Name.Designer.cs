﻿// <auto-generated />
using System;
using Bengbenz.Embassy.eServices.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Bengbenz.Embassy.eServices.Infrastructure.Migrations
{
    [DbContext(typeof(EmbassyDbContext))]
    [Migration("20240725221101_Fix_ServiceOffers_Table_Name")]
    partial class Fix_ServiceOffers_Table_Name
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.HasSequence("category_hilo")
                .IncrementsBy(10);

            modelBuilder.HasSequence("service_offer_hilo")
                .IncrementsBy(10);

            modelBuilder.Entity("Bengbenz.Embassy.eServices.Core.CategoryAggregrate.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "category_hilo");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int?>("ParentCategoryId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ParentCategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Bengbenz.Embassy.eServices.Core.ServiceOfferAggregrate.ServiceOffer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "service_offer_hilo");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<bool>("Featured")
                        .HasColumnType("boolean");

                    b.Property<string>("ImageUri")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("ServiceOffers");
                });

            modelBuilder.Entity("Bengbenz.Embassy.eServices.Core.CategoryAggregrate.Category", b =>
                {
                    b.HasOne("Bengbenz.Embassy.eServices.Core.CategoryAggregrate.Category", null)
                        .WithMany("SubCategories")
                        .HasForeignKey("ParentCategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Bengbenz.Embassy.eServices.Core.ServiceOfferAggregrate.ServiceOffer", b =>
                {
                    b.HasOne("Bengbenz.Embassy.eServices.Core.CategoryAggregrate.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Bengbenz.Embassy.eServices.Core.CategoryAggregrate.Category", b =>
                {
                    b.Navigation("SubCategories");
                });
#pragma warning restore 612, 618
        }
    }
}
