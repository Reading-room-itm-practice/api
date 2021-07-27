using Storage.Interfaces;
using Storage.Models.Follows;
using Storage.Models.Photos;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storage.Models
{
    [Table("Authors")]
    public class Author : AuditableModel, IDbMasterKey, IFollowable, IApproveable
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Biography { get; set; }
        public int? MainPhotoId { get; set; }
        public AuthorPhoto MainPhoto { get; set; }
        public ICollection<Book> Books { get; set; }
        public ICollection<AuthorPhoto> Photos { get; set; }
        public virtual ICollection<AuthorFollow> Followers { get; set; }
        public bool Approved { get; set; }
    }
}
