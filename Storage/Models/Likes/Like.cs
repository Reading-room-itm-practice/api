using Storage.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storage.Models.Likes
{
    [Table("Likes")]
    public abstract class Like : AuditableModel, IDbMasterKey<int>
    {
        [Key]
        public int Id { get; set; }
        public LikeableTypes LikeableType { get; set; }
    }
}
