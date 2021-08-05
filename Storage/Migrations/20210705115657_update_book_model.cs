using Microsoft.EntityFrameworkCore.Migrations;

namespace Storage.Migrations
{
    public partial class update_book_model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_books_authors_AuthorId",
                table: "books");

            migrationBuilder.DropForeignKey(
                name: "FK_books_categories_CategoryId",
                table: "books");

            migrationBuilder.DropForeignKey(
                name: "FK_books_Photo_MainPhotoId1",
                table: "books");

            migrationBuilder.DropForeignKey(
                name: "FK_follows_AspNetUsers_UserId",
                table: "follows");

            migrationBuilder.DropForeignKey(
                name: "FK_follows_AspNetUsers_UserId1",
                table: "follows");

            migrationBuilder.DropForeignKey(
                name: "FK_follows_authors_AuthorId",
                table: "follows");

            migrationBuilder.DropForeignKey(
                name: "FK_follows_categories_CategoryId",
                table: "follows");

            migrationBuilder.DropForeignKey(
                name: "FK_likes_AspNetUsers_UserId",
                table: "likes");

            migrationBuilder.DropForeignKey(
                name: "FK_likes_review_comments_ReviewCommentId",
                table: "likes");

            migrationBuilder.DropForeignKey(
                name: "FK_likes_reviews_ReviewId",
                table: "likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Photo_books_BookId",
                table: "Photo");

            migrationBuilder.DropForeignKey(
                name: "FK_read_statuses_AspNetUsers_UserId",
                table: "read_statuses");

            migrationBuilder.DropForeignKey(
                name: "FK_read_statuses_books_BookId",
                table: "read_statuses");

            migrationBuilder.DropForeignKey(
                name: "FK_review_comments_AspNetUsers_UserId",
                table: "review_comments");

            migrationBuilder.DropForeignKey(
                name: "FK_review_comments_reviews_ReviewId",
                table: "review_comments");

            migrationBuilder.DropForeignKey(
                name: "FK_reviews_AspNetUsers_UserId",
                table: "reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_reviews_books_BookId",
                table: "reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_suggestions",
                table: "suggestions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_reviews",
                table: "reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_review_comments",
                table: "review_comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_read_statuses",
                table: "read_statuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_notifications",
                table: "notifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_likes",
                table: "likes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_friend_requests",
                table: "friend_requests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_follows",
                table: "follows");

            migrationBuilder.DropPrimaryKey(
                name: "PK_categories",
                table: "categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_books",
                table: "books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_authors",
                table: "authors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Photo",
                table: "Photo");

            migrationBuilder.RenameTable(
                name: "suggestions",
                newName: "Suggestions");

            migrationBuilder.RenameTable(
                name: "reviews",
                newName: "Reviews");

            migrationBuilder.RenameTable(
                name: "review_comments",
                newName: "Review_comments");

            migrationBuilder.RenameTable(
                name: "read_statuses",
                newName: "Read_statuses");

            migrationBuilder.RenameTable(
                name: "notifications",
                newName: "Notifications");

            migrationBuilder.RenameTable(
                name: "likes",
                newName: "Likes");

            migrationBuilder.RenameTable(
                name: "friend_requests",
                newName: "Friend_requests");

            migrationBuilder.RenameTable(
                name: "follows",
                newName: "Follows");

            migrationBuilder.RenameTable(
                name: "categories",
                newName: "Categories");

            migrationBuilder.RenameTable(
                name: "books",
                newName: "Books");

            migrationBuilder.RenameTable(
                name: "authors",
                newName: "Authors");

            migrationBuilder.RenameTable(
                name: "Photo",
                newName: "Photos");

            migrationBuilder.RenameIndex(
                name: "IX_reviews_UserId",
                table: "Reviews",
                newName: "IX_Reviews_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_reviews_BookId",
                table: "Reviews",
                newName: "IX_Reviews_BookId");

            migrationBuilder.RenameIndex(
                name: "IX_review_comments_UserId",
                table: "Review_comments",
                newName: "IX_Review_comments_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_review_comments_ReviewId",
                table: "Review_comments",
                newName: "IX_Review_comments_ReviewId");

            migrationBuilder.RenameIndex(
                name: "IX_read_statuses_UserId",
                table: "Read_statuses",
                newName: "IX_Read_statuses_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_likes_UserId",
                table: "Likes",
                newName: "IX_Likes_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_likes_ReviewId",
                table: "Likes",
                newName: "IX_Likes_ReviewId");

            migrationBuilder.RenameIndex(
                name: "IX_likes_ReviewCommentId",
                table: "Likes",
                newName: "IX_Likes_ReviewCommentId");

            migrationBuilder.RenameIndex(
                name: "IX_follows_UserId1",
                table: "Follows",
                newName: "IX_Follows_UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_follows_UserId",
                table: "Follows",
                newName: "IX_Follows_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_follows_CategoryId",
                table: "Follows",
                newName: "IX_Follows_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_follows_AuthorId",
                table: "Follows",
                newName: "IX_Follows_AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_books_MainPhotoId1",
                table: "Books",
                newName: "IX_Books_MainPhotoId1");

            migrationBuilder.RenameIndex(
                name: "IX_books_CategoryId",
                table: "Books",
                newName: "IX_Books_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_books_AuthorId",
                table: "Books",
                newName: "IX_Books_AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Photo_BookId",
                table: "Photos",
                newName: "IX_Photos_BookId");

            migrationBuilder.AddColumn<int>(
                name: "ReleaseYear",
                table: "Books",
                type: "int",
                precision: 4,
                scale: 0,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Suggestions",
                table: "Suggestions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Review_comments",
                table: "Review_comments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Read_statuses",
                table: "Read_statuses",
                columns: new[] { "BookId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notifications",
                table: "Notifications",
                column: "Id");

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

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Books",
                table: "Books",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Authors",
                table: "Authors",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Photos",
                table: "Photos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Authors_AuthorId",
                table: "Books",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Categories_CategoryId",
                table: "Books",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Photos_MainPhotoId1",
                table: "Books",
                column: "MainPhotoId1",
                principalTable: "Photos",
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

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Books_BookId",
                table: "Photos",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Read_statuses_AspNetUsers_UserId",
                table: "Read_statuses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Read_statuses_Books_BookId",
                table: "Read_statuses",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Review_comments_AspNetUsers_UserId",
                table: "Review_comments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Review_comments_Reviews_ReviewId",
                table: "Review_comments",
                column: "ReviewId",
                principalTable: "Reviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AspNetUsers_UserId",
                table: "Reviews",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Books_BookId",
                table: "Reviews",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Authors_AuthorId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Categories_CategoryId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Photos_MainPhotoId1",
                table: "Books");

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

            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Books_BookId",
                table: "Photos");

            migrationBuilder.DropForeignKey(
                name: "FK_Read_statuses_AspNetUsers_UserId",
                table: "Read_statuses");

            migrationBuilder.DropForeignKey(
                name: "FK_Read_statuses_Books_BookId",
                table: "Read_statuses");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_comments_AspNetUsers_UserId",
                table: "Review_comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_comments_Reviews_ReviewId",
                table: "Review_comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AspNetUsers_UserId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Books_BookId",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Suggestions",
                table: "Suggestions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Review_comments",
                table: "Review_comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Read_statuses",
                table: "Read_statuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notifications",
                table: "Notifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Likes",
                table: "Likes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Friend_requests",
                table: "Friend_requests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Follows",
                table: "Follows");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Books",
                table: "Books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Authors",
                table: "Authors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Photos",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "ReleaseYear",
                table: "Books");

            migrationBuilder.RenameTable(
                name: "Suggestions",
                newName: "suggestions");

            migrationBuilder.RenameTable(
                name: "Reviews",
                newName: "reviews");

            migrationBuilder.RenameTable(
                name: "Review_comments",
                newName: "review_comments");

            migrationBuilder.RenameTable(
                name: "Read_statuses",
                newName: "read_statuses");

            migrationBuilder.RenameTable(
                name: "Notifications",
                newName: "notifications");

            migrationBuilder.RenameTable(
                name: "Likes",
                newName: "likes");

            migrationBuilder.RenameTable(
                name: "Friend_requests",
                newName: "friend_requests");

            migrationBuilder.RenameTable(
                name: "Follows",
                newName: "follows");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "categories");

            migrationBuilder.RenameTable(
                name: "Books",
                newName: "books");

            migrationBuilder.RenameTable(
                name: "Authors",
                newName: "authors");

            migrationBuilder.RenameTable(
                name: "Photos",
                newName: "Photo");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_UserId",
                table: "reviews",
                newName: "IX_reviews_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_BookId",
                table: "reviews",
                newName: "IX_reviews_BookId");

            migrationBuilder.RenameIndex(
                name: "IX_Review_comments_UserId",
                table: "review_comments",
                newName: "IX_review_comments_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Review_comments_ReviewId",
                table: "review_comments",
                newName: "IX_review_comments_ReviewId");

            migrationBuilder.RenameIndex(
                name: "IX_Read_statuses_UserId",
                table: "read_statuses",
                newName: "IX_read_statuses_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Likes_UserId",
                table: "likes",
                newName: "IX_likes_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Likes_ReviewId",
                table: "likes",
                newName: "IX_likes_ReviewId");

            migrationBuilder.RenameIndex(
                name: "IX_Likes_ReviewCommentId",
                table: "likes",
                newName: "IX_likes_ReviewCommentId");

            migrationBuilder.RenameIndex(
                name: "IX_Follows_UserId1",
                table: "follows",
                newName: "IX_follows_UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_Follows_UserId",
                table: "follows",
                newName: "IX_follows_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Follows_CategoryId",
                table: "follows",
                newName: "IX_follows_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Follows_AuthorId",
                table: "follows",
                newName: "IX_follows_AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_MainPhotoId1",
                table: "books",
                newName: "IX_books_MainPhotoId1");

            migrationBuilder.RenameIndex(
                name: "IX_Books_CategoryId",
                table: "books",
                newName: "IX_books_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_AuthorId",
                table: "books",
                newName: "IX_books_AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Photos_BookId",
                table: "Photo",
                newName: "IX_Photo_BookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_suggestions",
                table: "suggestions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_reviews",
                table: "reviews",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_review_comments",
                table: "review_comments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_read_statuses",
                table: "read_statuses",
                columns: new[] { "BookId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_notifications",
                table: "notifications",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_likes",
                table: "likes",
                columns: new[] { "LikeableId", "LikeableType" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_friend_requests",
                table: "friend_requests",
                columns: new[] { "FromId", "ToId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_follows",
                table: "follows",
                columns: new[] { "FollowableId", "FollowableType" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_categories",
                table: "categories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_books",
                table: "books",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_authors",
                table: "authors",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Photo",
                table: "Photo",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_books_authors_AuthorId",
                table: "books",
                column: "AuthorId",
                principalTable: "authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_books_categories_CategoryId",
                table: "books",
                column: "CategoryId",
                principalTable: "categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_books_Photo_MainPhotoId1",
                table: "books",
                column: "MainPhotoId1",
                principalTable: "Photo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_follows_AspNetUsers_UserId",
                table: "follows",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_follows_AspNetUsers_UserId1",
                table: "follows",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_follows_authors_AuthorId",
                table: "follows",
                column: "AuthorId",
                principalTable: "authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_follows_categories_CategoryId",
                table: "follows",
                column: "CategoryId",
                principalTable: "categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_likes_AspNetUsers_UserId",
                table: "likes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_likes_review_comments_ReviewCommentId",
                table: "likes",
                column: "ReviewCommentId",
                principalTable: "review_comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_likes_reviews_ReviewId",
                table: "likes",
                column: "ReviewId",
                principalTable: "reviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Photo_books_BookId",
                table: "Photo",
                column: "BookId",
                principalTable: "books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_read_statuses_AspNetUsers_UserId",
                table: "read_statuses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_read_statuses_books_BookId",
                table: "read_statuses",
                column: "BookId",
                principalTable: "books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_review_comments_AspNetUsers_UserId",
                table: "review_comments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_review_comments_reviews_ReviewId",
                table: "review_comments",
                column: "ReviewId",
                principalTable: "reviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_reviews_AspNetUsers_UserId",
                table: "reviews",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_reviews_books_BookId",
                table: "reviews",
                column: "BookId",
                principalTable: "books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
