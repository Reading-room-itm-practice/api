using Storage.Identity;
using Storage.Iterfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storage.Models.Likes
{
    [Table("Likes")]
    public class Like : AuditableModel, IDbMasterKey
    {
        [Key]
        public int Id { get; set; }
        public string LikeableType { get; set; }
    }
}
