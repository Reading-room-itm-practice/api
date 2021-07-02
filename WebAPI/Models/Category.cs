using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Common;

namespace WebAPI.Models
{
    [Table("categories")]
    public class Category : AuditableModel, IDbModel, IDbMasterKey, IFollowable
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Follow> Followers { get; set; }
    }
}
