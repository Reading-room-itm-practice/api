using Storage.Iterfaces;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storage.Models
{
    [Table("Friend_requests")]
    public class FriendRequest: IDbModel
    {
        public Guid FromId { get; set; }
        public Guid ToId { get; set; }
    }
}
