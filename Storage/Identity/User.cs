using Microsoft.AspNetCore.Identity;
using Storage.Iterfaces;
using Storage.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storage.Identity
{
    [Table("Users")]
    public class User : IdentityUser<int>, IFollowable
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
