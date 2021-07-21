using Storage.Iterfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storage.Models
{
    [Table("Review_comments")]
    public class ReviewComment : AuditableModel, IDbModel, IDbMasterKey, ILikeable
    {
        [Key]
        public int Id { get; set; }
        public int ReviewId { get; set; }
        public Review Review { get; set; }
        public string Content { get; set; }
        public ICollection<Like> Likes { get; set; }
    }
}
