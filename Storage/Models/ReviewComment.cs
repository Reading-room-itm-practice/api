using Storage.Interfaces;
using Storage.Models.Likes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storage.Models
{
    [Table("Review_comments")]
    public class ReviewComment : AuditableModel, ILikeable<ReviewCommentLike>, IDbMasterKey<int>
    {
        [Key]
        public int Id { get; set; }
        public int ReviewId { get; set; }
        public Review Review { get; set; }
        public string Content { get; set; }
        public virtual ICollection<ReviewCommentLike> Likes { get; set; }
    }
}
