using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ṃeenkaran.Migrations
{
    /// <inheritdoc />
    public partial class AddguidePaymentModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLoginOtpUsed",
                table: "Guides",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LoginOtp",
                table: "Guides",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LoginOtpExpiryTime",
                table: "Guides",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GuidePayments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    GuideId = table.Column<int>(type: "int", nullable: false),
                    GuideAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PlatformFeePercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PlatformFeeFromGuide = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UserServiceCharge = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalUserPays = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GuideReceives = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RazorpayOrderId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RazorpayPaymentId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RazorpaySignature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaidAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuidePayments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlatformPaymentSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GuidePlatformFeePercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UserServiceCharge = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedByAdminId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlatformPaymentSettings", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GuidePayments");

            migrationBuilder.DropTable(
                name: "PlatformPaymentSettings");

            migrationBuilder.DropColumn(
                name: "IsLoginOtpUsed",
                table: "Guides");

            migrationBuilder.DropColumn(
                name: "LoginOtp",
                table: "Guides");

            migrationBuilder.DropColumn(
                name: "LoginOtpExpiryTime",
                table: "Guides");
        }
    }
}
