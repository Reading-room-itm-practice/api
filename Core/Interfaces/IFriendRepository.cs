using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IFriendRepository : IBaseRepository<FriendRequest>
    {
        public Task<IEnumerable<FriendRequest>> GetApprovedSentAndReceivedFriendRequests(Guid userId);
        public Task<FriendRequest> GetApprovedSentAndReceivedFriendRequest(Guid userId);
    }
}
