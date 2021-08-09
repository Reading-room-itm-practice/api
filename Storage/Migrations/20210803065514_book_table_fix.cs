using Microsoft.EntityFrameworkCore.Migrations;

namespace Storage.Migrations
{
    public partial class book_table_fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RelaseDate",
                table: "Books",
                newName: "ReleaseDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReleaseDate",
                table: "Books",
                newName: "RelaseDate");
        }
    }
}
