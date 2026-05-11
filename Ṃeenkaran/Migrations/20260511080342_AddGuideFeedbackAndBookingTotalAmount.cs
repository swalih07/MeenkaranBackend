using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ṃeenkaran.Migrations
{
    /// <inheritdoc />
    public partial class AddGuideFeedbackAndBookingTotalAmount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotelAmount",
                table: "GuideBookings",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "GuideFeedbacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GuideId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    GuideBookingId = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuideFeedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GuideFeedbacks_GuideBookings_GuideBookingId",
                        column: x => x.GuideBookingId,
                        principalTable: "GuideBookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GuideFeedbacks_Guides_GuideId",
                        column: x => x.GuideId,
                        principalTable: "Guides",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GuideFeedbacks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GuideFeedbacks_GuideBookingId",
                table: "GuideFeedbacks",
                column: "GuideBookingId");

            migrationBuilder.CreateIndex(
                name: "IX_GuideFeedbacks_GuideId",
                table: "GuideFeedbacks",
                column: "GuideId");

            migrationBuilder.CreateIndex(
                name: "IX_GuideFeedbacks_UserId",
                table: "GuideFeedbacks",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GuideFeedbacks");

            migrationBuilder.DropColumn(
                name: "TotelAmount",
                table: "GuideBookings");
        }
    }
}
