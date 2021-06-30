using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebAPI.Models;

namespace WebAPI.Identity
{
    [Table("users")]
    public class User : IdentityUser<int>
    {
        public ICollection<Follow> Followings { get; set; }
        public ICollection<Follow> Followers { get; set; }
        public ICollection<User> Friends { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<ReviewComment> ReviewComments { get; set; }
        public ICollection<ReadStatus> ReadStatuses { get; set; }
        public ICollection<Like> Likes { get; set; }
    }
}
