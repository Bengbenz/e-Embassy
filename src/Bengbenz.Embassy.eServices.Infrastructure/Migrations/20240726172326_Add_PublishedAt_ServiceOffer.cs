using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bengbenz.Embassy.eServices.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_PublishedAt_ServiceOffer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Featured",
                table: "ServiceOffers",
                newName: "IsFeatured");

            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                table: "ServiceOffers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "PublishedAt",
                table: "ServiceOffers",
                type: "timestamp without time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublished",
                table: "ServiceOffers");

            migrationBuilder.DropColumn(
                name: "PublishedAt",
                table: "ServiceOffers");

            migrationBuilder.RenameColumn(
                name: "IsFeatured",
                table: "ServiceOffers",
                newName: "Featured");
        }
    }
}
