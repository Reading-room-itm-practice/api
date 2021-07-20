using Storage.Iterfaces;
using Storage.Models.Follows;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storage.Models
{
    [Table("Categories")]
    public class Category : AuditableModel, IDbMasterKey, IFollowable
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<CategoryFollow> Followers { get; set; }
    }
}
