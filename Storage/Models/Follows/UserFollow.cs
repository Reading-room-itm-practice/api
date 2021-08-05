using Storage.Identity;
using System;

namespace Storage.Models.Follows
{
    public class UserFollow : Follow
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
