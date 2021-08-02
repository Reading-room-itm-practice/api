using Storage.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storage.Models.Photos
{
    [Table("Photos")]
    public abstract class Photo : AuditableModel, IDbMasterKey<int>
    {
        [Key]
        public int Id { get; set; }
        public PhotoTypes PhotoType { get; set; }
        public string Path { get; set; }
    }
}
