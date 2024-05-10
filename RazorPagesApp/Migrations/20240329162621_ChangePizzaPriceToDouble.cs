using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PizzaShop.Migrations
{
    public partial class ChangePizzaPriceToDouble : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_pizzaOrders",
                table: "pizzaOrders");

            migrationBuilder.RenameTable(
                name: "pizzaOrders",
                newName: "PizzaOrders");

            migrationBuilder.AlterColumn<double>(
                name: "PizzaPrice",
                table: "PizzaOrders",
                type: "float",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PizzaOrders",
                table: "PizzaOrders",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PizzaOrders",
                table: "PizzaOrders");

            migrationBuilder.RenameTable(
                name: "PizzaOrders",
                newName: "pizzaOrders");

            migrationBuilder.AlterColumn<float>(
                name: "PizzaPrice",
                table: "pizzaOrders",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddPrimaryKey(
                name: "PK_pizzaOrders",
                table: "pizzaOrders",
                column: "Id");
        }
    }
}
