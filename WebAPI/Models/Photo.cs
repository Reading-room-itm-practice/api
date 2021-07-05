using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Common;

namespace WebAPI.Models
{
    [Table("Photos")]
    public class Photo : AuditableModel, IDbModel, IDbMasterKey
    {
        [Key]
        public int Id { get; set; }
        public int BookId { get; set; }
        public string Path { get; set; }
    }
}
