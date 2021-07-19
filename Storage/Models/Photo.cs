using Storage.Iterfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storage.Models
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
