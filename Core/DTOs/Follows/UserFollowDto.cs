using System;

namespace Core.DTOs.Follows
{
    public class UserFollowDto : FollowDto
    {
        public new Guid FollowableId { get; set; }
    }
}
