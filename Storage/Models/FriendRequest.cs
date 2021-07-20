using Storage.Identity;
using Storage.Iterfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storage.Models
{
    [Table("Friend_requests")]
    public class FriendRequest : IDbMasterKey
    {
        [Key]
        public int Id { get; set; }
        public int FromId { get; set; }
        public User From { get; set; }
        public int ToId { get; set; }
        public User To { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
