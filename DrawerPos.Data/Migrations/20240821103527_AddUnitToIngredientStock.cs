using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DrawerPos.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUnitToIngredientStock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UnitId",
                table: "IngredientStocks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_IngredientStocks_UnitId",
                table: "IngredientStocks",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientStocks_Units_UnitId",
                table: "IngredientStocks",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "UnitId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredientStocks_Units_UnitId",
                table: "IngredientStocks");

            migrationBuilder.DropIndex(
                name: "IX_IngredientStocks_UnitId",
                table: "IngredientStocks");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "IngredientStocks");
        }
    }
}
