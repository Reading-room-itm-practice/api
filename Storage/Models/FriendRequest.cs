using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Storage.Iterfaces;

namespace Storage.Models
{
    [Table("Friend_requests")]
    public class FriendRequest: IDbModel
    {
        public int FromId { get; set; }
        public int ToId { get; set; }
    }
}
