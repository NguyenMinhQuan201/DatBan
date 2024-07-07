using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class Intit77 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Payment",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "TableID",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "OrderDetails");

            migrationBuilder.RenameColumn(
                name: "DiscountID",
                table: "Orders",
                newName: "Status");

            migrationBuilder.AddColumn<int>(
                name: "RestaurantID",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RestaurantID",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Orders",
                newName: "DiscountID");

            migrationBuilder.AddColumn<double>(
                name: "Payment",
                table: "OrderDetails",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "TableID",
                table: "OrderDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "OrderDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
