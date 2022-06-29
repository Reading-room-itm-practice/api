using Core.Common;
using Storage.Interfaces;


namespace Core.Requests
{
    public class ApproveFriendRequest : IRequest, IApproveable
    {
        public bool Approved { get; set; }
    }
}
