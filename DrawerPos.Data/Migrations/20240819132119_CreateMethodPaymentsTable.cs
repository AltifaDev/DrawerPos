using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DrawerPos.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateMethodPaymentsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MethodPayments",
                columns: table => new
                {
                    QrId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MethodName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    MethodType = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    MethodNumber = table.Column<int>(type: "int", nullable: true),
                    MethodStatus = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MethodPayments", x => x.QrId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MethodPayments");
        }
    }
}
