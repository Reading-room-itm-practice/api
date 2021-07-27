using Microsoft.EntityFrameworkCore.Migrations;

namespace Storage.Migrations
{
    public partial class Suggestions_Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "Categories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "Books",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "Authors",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Approved",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Approved",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Approved",
                table: "Authors");
        }
    }
}
