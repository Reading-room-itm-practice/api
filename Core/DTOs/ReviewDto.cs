using Core.Common;
using System;

namespace Core.DTOs
{
    public class ReviewDto : LikeableDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int Stars { get; set; }
        public string Content { get; set; }
        public UserDto Creator { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastModifiedAt { get; set; }
    }
}
