using Storage.Identity;
using Storage.Iterfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storage.Models.Likes
{
    [Table("Likes")]
    public class Like : IDbMasterKey
    {
        [Key]
        public int Id { get; set; }
        public int LikerId { get; set; }
        public User Liker { get; set; }
        public string LikeableType { get; set; }
    }
}
