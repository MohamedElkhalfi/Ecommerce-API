using Microsoft.EntityFrameworkCore.Migrations;

namespace Ecommerce.DataAccess.Migrations
{
    public partial class Ecommerce13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Quantity",
                schema: "Ecommerce",
                table: "Product",
                type: "decimal(6, 2)",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                schema: "Ecommerce",
                table: "Product",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(6, 2)");
        }
    }
}
