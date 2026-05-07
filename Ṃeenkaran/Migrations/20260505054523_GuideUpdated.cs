using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ṃeenkaran.Migrations
{
    /// <inheritdoc />
    public partial class GuideUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Guides",
                newName: "IdProofUrl");

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Guides",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Guides",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Guides_UserId",
                table: "Guides",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Guides_Users_UserId",
                table: "Guides",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Guides_Users_UserId",
                table: "Guides");

            migrationBuilder.DropIndex(
                name: "IX_Guides_UserId",
                table: "Guides");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Guides");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Guides");

            migrationBuilder.RenameColumn(
                name: "IdProofUrl",
                table: "Guides",
                newName: "Name");
        }
    }
}
