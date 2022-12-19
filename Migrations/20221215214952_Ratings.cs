using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hongur.Migrations
{
    /// <inheritdoc />
    public partial class Ratings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    RestaurantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "DATETIME", maxLength: 255, nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Modified = table.Column<DateTime>(type: "DATETIME", maxLength: 255, nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ratings_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_RestaurantId",
                table: "Ratings",
                column: "RestaurantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ratings");
        }
    }
}
