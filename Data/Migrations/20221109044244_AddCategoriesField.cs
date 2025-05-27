using Microsoft.EntityFrameworkCore.Migrations;

namespace eCommerce.Data.Migrations
{
    public partial class AddCategoriesField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Categories",
                table: "Ad",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsBuying",
                table: "Ad",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Ad",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Categories",
                table: "Ad");

            migrationBuilder.DropColumn(
                name: "IsBuying",
                table: "Ad");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Ad");
        }
    }
}
