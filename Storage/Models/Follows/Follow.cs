using Storage.Identity;
using Storage.Iterfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storage.Models.Follows
{
    [Table("Follows")]
    public class Follow : AuditableModel, IDbMasterKey
    {
        [Key]
        public int Id { get; set; }
        public string FollowableType { get; set; }
    }
}
