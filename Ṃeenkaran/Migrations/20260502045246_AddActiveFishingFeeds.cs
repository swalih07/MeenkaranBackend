using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ṃeenkaran.Migrations
{
    /// <inheritdoc />
    public partial class AddActiveFishingFeeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PriceperDay",
                table: "Guides",
                newName: "PricePerDay");

            migrationBuilder.CreateTable(
                name: "ActiveFishingFeeds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpotName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Area = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FishType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActiveBoats = table.Column<int>(type: "int", nullable: false),
                    Weather = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsHotSpot = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GuideId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActiveFishingFeeds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActiveFishingFeeds_Guides_GuideId",
                        column: x => x.GuideId,
                        principalTable: "Guides",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GuidePackages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Duration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Includes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuideId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuidePackages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GuidePackages_Guides_GuideId",
                        column: x => x.GuideId,
                        principalTable: "Guides",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActiveFishingFeeds_GuideId",
                table: "ActiveFishingFeeds",
                column: "GuideId");

            migrationBuilder.CreateIndex(
                name: "IX_GuidePackages_GuideId",
                table: "GuidePackages",
                column: "GuideId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActiveFishingFeeds");

            migrationBuilder.DropTable(
                name: "GuidePackages");

            migrationBuilder.RenameColumn(
                name: "PricePerDay",
                table: "Guides",
                newName: "PriceperDay");
        }
    }
}
