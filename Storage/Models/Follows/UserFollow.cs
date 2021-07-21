using Storage.Identity;
using System;

namespace Storage.Models.Follows
{
    public class UserFollow : Follow
    {
        public Guid FollowingId { get; set; }
        public User Following { get; set; }
    }
}
