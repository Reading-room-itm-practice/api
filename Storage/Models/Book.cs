using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Storage.Iterfaces;

namespace Storage.Models
{
    [Table("Books")]
    public class Book : AuditableModel, IDbModel, IDbMasterKey
    {
        [Key]
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int MainPhotoId { get; set; }
        public Photo MainPhoto { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public string Description { get; set; }
        public ICollection<ReadStatus> ReadStatuses { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
