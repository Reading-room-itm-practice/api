using Storage.Iterfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storage.Models
{
    [Table("Friend_requests")]
    public class FriendRequest : IDbModel
    {
        public int FromId { get; set; }
        public int ToId { get; set; }
    }
}
