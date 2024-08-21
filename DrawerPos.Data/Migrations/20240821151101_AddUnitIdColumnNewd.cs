using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DrawerPos.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUnitIdColumnNewd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Add the UnitId column if it does not already exist
            migrationBuilder.AddColumn<int>(
                name: "UnitId",
                table: "IngredientStocks",
                nullable: false,
                defaultValue: 0);

            // Add the foreign key constraint
            migrationBuilder.AddForeignKey(
                name: "FK_IngredientStocks_Units_UnitId",
                table: "IngredientStocks",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "UnitId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop the foreign key constraint
            migrationBuilder.DropForeignKey(
                name: "FK_IngredientStocks_Units_UnitId",
                table: "IngredientStocks");

            // Drop the UnitId column
            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "IngredientStocks");
        }
    }
}