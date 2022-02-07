using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ecommerce.DataAccess.Migrations
{
    public partial class Ecommerce11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Ecommerce");

            migrationBuilder.CreateTable(
                name: "Category",
                schema: "Ecommerce",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Photo = table.Column<string>(nullable: true),
                    Description = table.Column<string>(unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                schema: "Ecommerce",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    Name = table.Column<DateTime>(unicode: false, maxLength: 20, nullable: false),
                    Description = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    CurrentPrice = table.Column<decimal>(type: "decimal(6, 2)", nullable: false),
                    Is_Promotion = table.Column<bool>(nullable: true),
                    Is_Selected = table.Column<bool>(nullable: true),
                    Is_Available = table.Column<bool>(nullable: true),
                    PhotoName = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Product_Category",
                        column: x => x.ID,
                        principalSchema: "Ecommerce",
                        principalTable: "Category",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product",
                schema: "Ecommerce");

            migrationBuilder.DropTable(
                name: "Category",
                schema: "Ecommerce");
        }
    }
}
