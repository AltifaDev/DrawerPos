using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DrawerPos.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddReorderPointToIngredientStock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_IngredientStocks",
                table: "IngredientStocks");

            migrationBuilder.AlterColumn<int>(
                name: "ReorderPoint",
                table: "IngredientStocks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "QuantityInStock",
                table: "IngredientStocks",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<int>(
                name: "IngredientStockId",
                table: "IngredientStocks",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirationDate",
                table: "IngredientStocks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "IngredientStocks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IngredientStocks",
                table: "IngredientStocks",
                column: "IngredientStockId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientStocks_IngredientId",
                table: "IngredientStocks",
                column: "IngredientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_IngredientStocks",
                table: "IngredientStocks");

            migrationBuilder.DropIndex(
                name: "IX_IngredientStocks_IngredientId",
                table: "IngredientStocks");

            migrationBuilder.DropColumn(
                name: "IngredientStockId",
                table: "IngredientStocks");

            migrationBuilder.DropColumn(
                name: "ExpirationDate",
                table: "IngredientStocks");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "IngredientStocks");

            migrationBuilder.AlterColumn<decimal>(
                name: "ReorderPoint",
                table: "IngredientStocks",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "QuantityInStock",
                table: "IngredientStocks",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IngredientStocks",
                table: "IngredientStocks",
                column: "IngredientId");
        }
    }
}
