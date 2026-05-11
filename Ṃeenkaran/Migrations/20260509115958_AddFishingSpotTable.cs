using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ṃeenkaran.Migrations
{
    /// <inheritdoc />
    public partial class AddFishingSpotTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "FishingSpots",
                newName: "SpotName");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "FishingSpots",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "GuideId",
                table: "FishingSpots",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsHotspot",
                table: "FishingSpots",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LocationName",
                table: "FishingSpots",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "FishingSpots",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FishingSpots_GuideId",
                table: "FishingSpots",
                column: "GuideId");

            migrationBuilder.AddForeignKey(
                name: "FK_FishingSpots_Guides_GuideId",
                table: "FishingSpots",
                column: "GuideId",
                principalTable: "Guides",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FishingSpots_Guides_GuideId",
                table: "FishingSpots");

            migrationBuilder.DropIndex(
                name: "IX_FishingSpots_GuideId",
                table: "FishingSpots");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "FishingSpots");

            migrationBuilder.DropColumn(
                name: "GuideId",
                table: "FishingSpots");

            migrationBuilder.DropColumn(
                name: "IsHotspot",
                table: "FishingSpots");

            migrationBuilder.DropColumn(
                name: "LocationName",
                table: "FishingSpots");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "FishingSpots");

            migrationBuilder.RenameColumn(
                name: "SpotName",
                table: "FishingSpots",
                newName: "Name");
        }
    }
}
