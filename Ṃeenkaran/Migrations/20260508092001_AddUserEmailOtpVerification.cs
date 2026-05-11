using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ṃeenkaran.Migrations
{
    /// <inheritdoc />
    public partial class AddUserEmailOtpVerification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmailOtp",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EmailOtpExpiryTime",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsEmailOtpUsed",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsEmailVerified",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailOtp",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EmailOtpExpiryTime",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsEmailOtpUsed",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsEmailVerified",
                table: "Users");
        }
    }
}
