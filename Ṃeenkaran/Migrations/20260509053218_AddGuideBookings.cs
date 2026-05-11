using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ṃeenkaran.Migrations
{
    /// <inheritdoc />
    public partial class AddGuideBookings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "GuideBookings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "GuideBookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PersonCount",
                table: "GuideBookings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "GuideBookings");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "GuideBookings");

            migrationBuilder.DropColumn(
                name: "PersonCount",
                table: "GuideBookings");
        }
    }
}
