using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ecommerce.DataAccess.Migrations
{
    public partial class Ecommerce12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "Ecommerce",
                table: "Product",
                unicode: false,
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldUnicode: false,
                oldMaxLength: 20);

            migrationBuilder.CreateTable(
                name: "Client",
                schema: "Ecommerce",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Email = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    Address = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    PhoneNumber = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    Username = table.Column<string>(unicode: false, maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                schema: "Ecommerce",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    clientID = table.Column<int>(nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(6, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Order_Client_clientID",
                        column: x => x.clientID,
                        principalSchema: "Ecommerce",
                        principalTable: "Client",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                schema: "Ecommerce",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Product_ID = table.Column<int>(nullable: true),
                    Quantity = table.Column<decimal>(type: "decimal(6, 2)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(6, 2)", nullable: false),
                    Order_ID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OrderItem_Order_Order_ID",
                        column: x => x.Order_ID,
                        principalSchema: "Ecommerce",
                        principalTable: "Order",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderItem_Product_Product_ID",
                        column: x => x.Product_ID,
                        principalSchema: "Ecommerce",
                        principalTable: "Product",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_clientID",
                schema: "Ecommerce",
                table: "Order",
                column: "clientID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_Order_ID",
                schema: "Ecommerce",
                table: "OrderItem",
                column: "Order_ID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_Product_ID",
                schema: "Ecommerce",
                table: "OrderItem",
                column: "Product_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItem",
                schema: "Ecommerce");

            migrationBuilder.DropTable(
                name: "Order",
                schema: "Ecommerce");

            migrationBuilder.DropTable(
                name: "Client",
                schema: "Ecommerce");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Name",
                schema: "Ecommerce",
                table: "Product",
                unicode: false,
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 20,
                oldNullable: true);
        }
    }
}
