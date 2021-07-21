using Storage.Iterfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storage.Models
{
    [Table("Likes")]
    public class Like : AuditableModel, IDbModel
    {
        public int LikeableId { get; set; }
        public string LikeableType { get; set; }
    }
}
