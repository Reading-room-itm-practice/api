using Storage.Iterfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storage.Models
{
    [Table("Categories")]
    public class Category : AuditableModel, IDbModel, IDbMasterKey, IFollowable
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Follow> Followers { get; set; }
    }
}
