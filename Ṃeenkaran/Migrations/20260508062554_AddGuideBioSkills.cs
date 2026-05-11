using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ṃeenkaran.Migrations
{
    /// <inheritdoc />
    public partial class AddGuideBioSkills : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "Guides",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsPasswordResetOtpUsed",
                table: "Guides",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PasswordResetOtp",
                table: "Guides",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PasswordResetOtpExpiryTime",
                table: "Guides",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Skills",
                table: "Guides",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bio",
                table: "Guides");

            migrationBuilder.DropColumn(
                name: "IsPasswordResetOtpUsed",
                table: "Guides");

            migrationBuilder.DropColumn(
                name: "PasswordResetOtp",
                table: "Guides");

            migrationBuilder.DropColumn(
                name: "PasswordResetOtpExpiryTime",
                table: "Guides");

            migrationBuilder.DropColumn(
                name: "Skills",
                table: "Guides");
        }
    }
}
