using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bengbenz.Embassy.eServices.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Fix_ServiceOffers_Table_Name : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceOffer_Categories_CategoryId",
                table: "ServiceOffer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceOffer",
                table: "ServiceOffer");

            migrationBuilder.RenameTable(
                name: "ServiceOffer",
                newName: "ServiceOffers");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceOffer_CategoryId",
                table: "ServiceOffers",
                newName: "IX_ServiceOffers_CategoryId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "ServiceOffers",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ServiceOffers",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceOffers",
                table: "ServiceOffers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceOffers_Categories_CategoryId",
                table: "ServiceOffers",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceOffers_Categories_CategoryId",
                table: "ServiceOffers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceOffers",
                table: "ServiceOffers");

            migrationBuilder.RenameTable(
                name: "ServiceOffers",
                newName: "ServiceOffer");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceOffers_CategoryId",
                table: "ServiceOffer",
                newName: "IX_ServiceOffer_CategoryId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "ServiceOffer",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ServiceOffer",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceOffer",
                table: "ServiceOffer",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceOffer_Categories_CategoryId",
                table: "ServiceOffer",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
