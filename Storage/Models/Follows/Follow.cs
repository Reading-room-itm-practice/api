using Storage.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storage.Models.Follows
{
    [Table("Follows")]
    public abstract class Follow : AuditableModel, IDbMasterKey<int>
    {
        [Key]
        public int Id { get; set; }
        public FollowableTypes FollowableType { get; set; }
    }
}
