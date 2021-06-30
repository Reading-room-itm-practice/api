using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    [Table("friend_requests")]
    public class FriendRequest
    {
        public int FromId { get; set; }
        public int ToId { get; set; }
    }
}
