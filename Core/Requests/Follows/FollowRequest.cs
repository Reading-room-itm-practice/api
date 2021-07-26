using Core.Common;

namespace Core.Requests.Follows
{
    public class FollowRequest : IRequest
    {
        public int FollowableId { get; set; }
    }
}
