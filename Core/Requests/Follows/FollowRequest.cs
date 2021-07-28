using Core.Common;
using System;

namespace Core.Requests.Follows
{
    public class FollowRequest : IRequest
    {
        public Guid CreatorId { get; set; }
        public int FollowableId { get; set; }
    }
}
