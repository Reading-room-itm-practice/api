using Core.Common;
using System;

namespace Core.DTOs
{
    public class ReviewCommentDto : LikeableDto
    {
        public int Id { get; set; }
        public int ReviewId { get; set; }
        public string Content { get; set; }
        public UserDto Creator { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastModifiedAt { get; set; }
    }
}
