using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PizzaShop.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "pizzaOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PizzaName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PizzaPrice = table.Column<float>(type: "real", nullable: false),
                    TomatoSauce = table.Column<bool>(type: "bit", nullable: false),
                    Cheese = table.Column<bool>(type: "bit", nullable: false),
                    Pepperoni = table.Column<bool>(type: "bit", nullable: false),
                    Mushroom = table.Column<bool>(type: "bit", nullable: false),
                    Tuna = table.Column<bool>(type: "bit", nullable: false),
                    Ham = table.Column<bool>(type: "bit", nullable: false),
                    Beef = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pizzaOrders", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pizzaOrders");
        }
    }
}
