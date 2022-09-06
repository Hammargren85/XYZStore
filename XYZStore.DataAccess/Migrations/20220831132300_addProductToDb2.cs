using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XYZStore.Migrations
{
    public partial class addProductToDb2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Price10",
                table: "Products",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Price5",
                table: "Products",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price10",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Price5",
                table: "Products");
        }
    }
}
