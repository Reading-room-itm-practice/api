using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Core.Common;

namespace Core.Models
{
    [Table("Reviews")]
    public class Review : AuditableModel, IDbModel, IDbMasterKey, ILikeable
    {
        [Key]
        public int Id { get; set; }
        public int BookId { get; set; }
        public int Stars { get; set; }
        public string Content { get; set; }
        public ICollection<ReviewComment> Comments { get; set; }
        public ICollection<Like> Likes { get; set; }
    }
}
