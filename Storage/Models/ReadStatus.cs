using Storage.Identity;
using Storage.Iterfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storage.Models
{
    [Table("Read_statuses")]
    public class ReadStatus : IDbModel
    {
        public int BookId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public bool IsRead { get; set; }
        public bool IsWantRead { get; set; }
        public bool IsFavorite { get; set; }
    }
}
