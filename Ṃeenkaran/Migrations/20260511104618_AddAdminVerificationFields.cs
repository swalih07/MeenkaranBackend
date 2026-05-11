using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ṃeenkaran.Migrations
{
    /// <inheritdoc />
    public partial class AddAdminVerificationFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BlockReason",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsBlocked",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "BlockReason",
                table: "Guides",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsBlocked",
                table: "Guides",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRejected",
                table: "Guides",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "RejectionReason",
                table: "Guides",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlockReason",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsBlocked",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "BlockReason",
                table: "Guides");

            migrationBuilder.DropColumn(
                name: "IsBlocked",
                table: "Guides");

            migrationBuilder.DropColumn(
                name: "IsRejected",
                table: "Guides");

            migrationBuilder.DropColumn(
                name: "RejectionReason",
                table: "Guides");
        }
    }
}
