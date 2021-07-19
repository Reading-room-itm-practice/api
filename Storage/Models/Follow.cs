using Storage.Iterfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storage.Models
{
    [Table("Follows")]
    public class Follow : AuditableModel, IDbModel
    {
        public int FollowableId { get; set; }
        public string FollowableType { get; set; }
    }
}
