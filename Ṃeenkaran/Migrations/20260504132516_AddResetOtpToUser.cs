using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ṃeenkaran.Migrations
{
    /// <inheritdoc />
    public partial class AddResetOtpToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResetOtp",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ResetOtpExpiry",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GuideReviews_UserId",
                table: "GuideReviews",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_GuideReviews_Users_UserId",
                table: "GuideReviews",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GuideReviews_Users_UserId",
                table: "GuideReviews");

            migrationBuilder.DropIndex(
                name: "IX_GuideReviews_UserId",
                table: "GuideReviews");

            migrationBuilder.DropColumn(
                name: "ResetOtp",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ResetOtpExpiry",
                table: "Users");
        }
    }
}
