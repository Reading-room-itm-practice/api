using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Storage.Iterfaces;

namespace Storage.Models
{
    [Table("Suggestions")]
    public class Suggestion : AuditableModel, IDbModel, IDbMasterKey
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Comment { get; set; }
    }
}
