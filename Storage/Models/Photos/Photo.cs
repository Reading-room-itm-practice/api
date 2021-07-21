using Storage.Iterfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storage.Models.Photos
{
    [Table("Photos")]
    public class Photo : AuditableModel, IDbMasterKey
    {
        [Key]
        public int Id { get; set; }
        public string PhotoType { get; set; }
        public string Path { get; set; }
    }
}
