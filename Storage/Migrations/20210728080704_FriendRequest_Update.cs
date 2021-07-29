using Microsoft.EntityFrameworkCore.Migrations;

namespace Storage.Migrations
{
    public partial class FriendRequest_Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsConfirmed",
                table: "Friend_requests");

            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "Friend_requests",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Approved",
                table: "Friend_requests");

            migrationBuilder.AddColumn<bool>(
                name: "IsConfirmed",
                table: "Friend_requests",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
