using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ṃeenkaran.Migrations
{
    /// <inheritdoc />
    public partial class AddGuidePackages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Duration",
                table: "GuidePackages",
                newName: "TripLocation");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "GuidePackages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DurationHours",
                table: "GuidePackages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "GuidePackages",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "GuidePackages");

            migrationBuilder.DropColumn(
                name: "DurationHours",
                table: "GuidePackages");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "GuidePackages");

            migrationBuilder.RenameColumn(
                name: "TripLocation",
                table: "GuidePackages",
                newName: "Duration");
        }
    }
}
