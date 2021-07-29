using Core.Common;
using Core.DTOs;
using Core.Requests;
using Core.ServiceResponses;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IFriendService : ICrudService<FriendRequest>
    {
        public Task<ServiceResponse<IEnumerable<IDto>>> GetReceivedFriendRequests<IDto>();
        public Task<ServiceResponse<IDto>> GetReceivedFriendRequest<IDto>(int id);
        public Task<ServiceResponse> SendFriendRequest(SendFriendRequest request);
        public Task<ServiceResponse<List<FriendDto>>> GetFriends();
        public Task<ServiceResponse<FriendDto>> GetFriend(Guid id);
        public Task<ServiceResponse> AcceptFriendRequest(int friendRequestId);
    }
}
