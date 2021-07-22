using Storage.Iterfaces;
using Storage.Models.Photos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storage.Models
{
    [Table("Books")]
    public class Book : AuditableModel, IDbMasterKey
    {
        [Key]
        public int Id { get; set; }
        public int? AuthorId { get; set; }
        public Author Author { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public int? MainPhotoId { get; set; }
        public BookPhoto MainPhoto { get; set; }
        public ICollection<BookPhoto> Photos { get; set; }
        public string Title { get; set; }
        public DateTime? RelaseDate { get; set; }
        public string Description { get; set; }
        public ICollection<ReadStatus> ReadStatuses { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
