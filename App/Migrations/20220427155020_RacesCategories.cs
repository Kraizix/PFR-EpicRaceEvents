using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Migrations
{
    public partial class RacesCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Races_RaceId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_RaceId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "RaceId",
                table: "Categories");

            migrationBuilder.CreateTable(
                name: "RacesCategories",
                columns: table => new
                {
                    AuhtorizedCategoriesId = table.Column<int>(type: "int", nullable: false),
                    RacesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RacesCategories", x => new { x.AuhtorizedCategoriesId, x.RacesId });
                    table.ForeignKey(
                        name: "FK_RacesCategories_Categories_AuhtorizedCategoriesId",
                        column: x => x.AuhtorizedCategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RacesCategories_Races_RacesId",
                        column: x => x.RacesId,
                        principalTable: "Races",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_RacesCategories_RacesId",
                table: "RacesCategories",
                column: "RacesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RacesCategories");

            migrationBuilder.AddColumn<int>(
                name: "RaceId",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_RaceId",
                table: "Categories",
                column: "RaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Races_RaceId",
                table: "Categories",
                column: "RaceId",
                principalTable: "Races",
                principalColumn: "Id");
        }
    }
}
