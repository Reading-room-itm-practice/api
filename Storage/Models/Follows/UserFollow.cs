using Storage.Identity;

namespace Storage.Models.Follows
{
    public class UserFollow : Follow
    {
        public int FollowingId { get; set; }
        public User Following { get; set; }
    }
}
