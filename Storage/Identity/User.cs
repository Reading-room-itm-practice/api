using Microsoft.AspNetCore.Identity;
using Storage.Iterfaces;
using Storage.Models;
using Storage.Models.Follows;
using Storage.Models.Likes;
using Storage.Models.Photos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storage.Identity
{
    [Table("Users")]
    public class User : IdentityUser<Guid>, IFollowable
    {
        public int? ProfilePhotoId { get; set; }
        public ProfilePhoto ProfilePhoto { get; set; }
        public ICollection<UserFollow> Followers { get; set; }
        public ICollection<UserFollow> FollowedUsers { get; set; }
        public ICollection<AuthorFollow> FollowedAuthors { get; set; }
        public ICollection<CategoryFollow> FollwedCategories { get; set; }
        public ICollection<FriendRequest> SentRequests { get; set; }
        public ICollection<FriendRequest> RecivedRequests { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<ReviewComment> ReviewComments { get; set; }
        public ICollection<ReadStatus> ReadStatuses { get; set; }   
        public ICollection<ReviewLike> ReviewLikes { get; set; }
        public ICollection<ReviewCommentLike> CommentLikes { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<Author> Authors { get; set; }
        public ICollection<Book> Books { get; set; }
        public ICollection<Suggestion> Suggestions { get; set; }
        public ICollection<Photo> AddedPhotos { get; set; }
        public ICollection<Notification> Notifications { get; set; }
    }
}
