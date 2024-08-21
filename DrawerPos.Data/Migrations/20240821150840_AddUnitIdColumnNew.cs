using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DrawerPos.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUnitIdColumnNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredientStocks_Units_UnitId",
                table: "IngredientStocks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IngredientStock",
                table: "IngredientStocks");

            migrationBuilder.DropIndex(
                name: "IX_IngredientStocks_UnitId",
                table: "IngredientStocks");

            migrationBuilder.AlterColumn<int>(
                name: "QuantityInStock",
                table: "IngredientStocks",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IngredientStocks",
                table: "IngredientStocks",
                column: "IngredientStockId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_IngredientStocks",
                table: "IngredientStocks");

            migrationBuilder.AlterColumn<decimal>(
                name: "QuantityInStock",
                table: "IngredientStocks",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IngredientStock",
                table: "IngredientStocks",
                column: "IngredientStockId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientStocks_UnitId",
                table: "IngredientStocks",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientStocks_Units_UnitId",
                table: "IngredientStocks",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "UnitId");
        }
    }
}
