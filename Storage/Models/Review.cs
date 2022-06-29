using Storage.Interfaces;
using Storage.Models.Likes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storage.Models
{
    [Table("Reviews")]
    public class Review : AuditableModel, IDbMasterKey, ILikeable, IApproveable
    {
        [Key]
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int Stars { get; set; }
        public string Content { get; set; }
        public ICollection<ReviewComment> Comments { get; set; }
        public ICollection<ReviewLike> Likes { get; set; }
        public bool Approved { get; set; }
    }
}
