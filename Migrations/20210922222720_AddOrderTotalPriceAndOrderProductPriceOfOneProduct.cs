using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineStore.Migrations
{
    public partial class AddOrderTotalPriceAndOrderProductPriceOfOneProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PriceOfOneProduct",
                table: "OrderProducts",
                type: "decimal(9,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "Order",
                type: "decimal(9,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceOfOneProduct",
                table: "OrderProducts");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Order");
        }
    }
}
