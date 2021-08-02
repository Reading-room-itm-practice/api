using System;

namespace Core.Requests.Follows
{
    public class UserFollowRequest : FollowRequest
    {
        public new Guid FollowableId { get; set; }
    }
}
