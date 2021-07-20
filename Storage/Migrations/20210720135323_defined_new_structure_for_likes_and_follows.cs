using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Storage.Migrations
{
    public partial class defined_new_structure_for_likes_and_follows : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_UserId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Follows_AspNetUsers_UserId",
                table: "Follows");

            migrationBuilder.DropForeignKey(
                name: "FK_Follows_AspNetUsers_UserId1",
                table: "Follows");

            migrationBuilder.DropForeignKey(
                name: "FK_Follows_Authors_AuthorId",
                table: "Follows");

            migrationBuilder.DropForeignKey(
                name: "FK_Follows_Categories_CategoryId",
                table: "Follows");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_AspNetUsers_UserId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Review_comments_ReviewCommentId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Reviews_ReviewId",
                table: "Likes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Likes",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Likes_UserId",
                table: "Likes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Friend_requests",
                table: "Friend_requests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Follows",
                table: "Follows");

            migrationBuilder.DropIndex(
                name: "IX_Follows_UserId",
                table: "Follows");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LikeableId",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "FollowableId",
                table: "Follows");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Follows");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Follows");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Follows");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "Likes",
                newName: "LikerId");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Likes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UserId1",
                table: "Follows",
                newName: "FollowingId");

            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "Follows",
                newName: "FollowerId");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Follows",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Follows_UserId1",
                table: "Follows",
                newName: "IX_Follows_FollowingId");

            migrationBuilder.AlterColumn<string>(
                name: "LikeableType",
                table: "Likes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Likes",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Friend_requests",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<bool>(
                name: "IsConfirmed",
                table: "Friend_requests",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "FollowableType",
                table: "Follows",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Follows",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Likes",
                table: "Likes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Friend_requests",
                table: "Friend_requests",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Follows",
                table: "Follows",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_LikerId",
                table: "Likes",
                column: "LikerId");

            migrationBuilder.CreateIndex(
                name: "IX_Friend_requests_FromId",
                table: "Friend_requests",
                column: "FromId");

            migrationBuilder.CreateIndex(
                name: "IX_Friend_requests_ToId",
                table: "Friend_requests",
                column: "ToId");

            migrationBuilder.CreateIndex(
                name: "IX_Follows_FollowerId",
                table: "Follows",
                column: "FollowerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Follows_AspNetUsers_FollowerId",
                table: "Follows",
                column: "FollowerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Follows_AspNetUsers_FollowingId",
                table: "Follows",
                column: "FollowingId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Follows_Authors_AuthorId",
                table: "Follows",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Follows_Categories_CategoryId",
                table: "Follows",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Friend_requests_AspNetUsers_FromId",
                table: "Friend_requests",
                column: "FromId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Friend_requests_AspNetUsers_ToId",
                table: "Friend_requests",
                column: "ToId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_AspNetUsers_LikerId",
                table: "Likes",
                column: "LikerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Review_comments_ReviewCommentId",
                table: "Likes",
                column: "ReviewCommentId",
                principalTable: "Review_comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Reviews_ReviewId",
                table: "Likes",
                column: "ReviewId",
                principalTable: "Reviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Follows_AspNetUsers_FollowerId",
                table: "Follows");

            migrationBuilder.DropForeignKey(
                name: "FK_Follows_AspNetUsers_FollowingId",
                table: "Follows");

            migrationBuilder.DropForeignKey(
                name: "FK_Follows_Authors_AuthorId",
                table: "Follows");

            migrationBuilder.DropForeignKey(
                name: "FK_Follows_Categories_CategoryId",
                table: "Follows");

            migrationBuilder.DropForeignKey(
                name: "FK_Friend_requests_AspNetUsers_FromId",
                table: "Friend_requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Friend_requests_AspNetUsers_ToId",
                table: "Friend_requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_AspNetUsers_LikerId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Review_comments_ReviewCommentId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Reviews_ReviewId",
                table: "Likes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Likes",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Likes_LikerId",
                table: "Likes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Friend_requests",
                table: "Friend_requests");

            migrationBuilder.DropIndex(
                name: "IX_Friend_requests_FromId",
                table: "Friend_requests");

            migrationBuilder.DropIndex(
                name: "IX_Friend_requests_ToId",
                table: "Friend_requests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Follows",
                table: "Follows");

            migrationBuilder.DropIndex(
                name: "IX_Follows_FollowerId",
                table: "Follows");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Friend_requests");

            migrationBuilder.DropColumn(
                name: "IsConfirmed",
                table: "Friend_requests");

            migrationBuilder.RenameColumn(
                name: "LikerId",
                table: "Likes",
                newName: "LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Likes",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "FollowingId",
                table: "Follows",
                newName: "UserId1");

            migrationBuilder.RenameColumn(
                name: "FollowerId",
                table: "Follows",
                newName: "LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Follows",
                newName: "CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_Follows_FollowingId",
                table: "Follows",
                newName: "IX_Follows_UserId1");

            migrationBuilder.AlterColumn<string>(
                name: "LikeableType",
                table: "Likes",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "Likes",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "LikeableId",
                table: "Likes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Likes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Likes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Likes",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FollowableType",
                table: "Follows",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "Follows",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "FollowableId",
                table: "Follows",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Follows",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Follows",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Follows",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Likes",
                table: "Likes",
                columns: new[] { "LikeableId", "LikeableType" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Friend_requests",
                table: "Friend_requests",
                columns: new[] { "FromId", "ToId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Follows",
                table: "Follows",
                columns: new[] { "FollowableId", "FollowableType" });

            migrationBuilder.CreateIndex(
                name: "IX_Likes_UserId",
                table: "Likes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Follows_UserId",
                table: "Follows",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserId",
                table: "AspNetUsers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_UserId",
                table: "AspNetUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Follows_AspNetUsers_UserId",
                table: "Follows",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Follows_AspNetUsers_UserId1",
                table: "Follows",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Follows_Authors_AuthorId",
                table: "Follows",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Follows_Categories_CategoryId",
                table: "Follows",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_AspNetUsers_UserId",
                table: "Likes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Review_comments_ReviewCommentId",
                table: "Likes",
                column: "ReviewCommentId",
                principalTable: "Review_comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Reviews_ReviewId",
                table: "Likes",
                column: "ReviewId",
                principalTable: "Reviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
