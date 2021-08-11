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
        public Task<IEnumerable<FriendRequest>> GetSentAndReceivedFriendRequests(Guid userId);
        public Task<IEnumerable<FriendRequest>> GetSentFriendRequests(Guid userId);
        public Task<IEnumerable<FriendRequest>> GetReceivedFriendRequests(Guid userId);
    }
}
