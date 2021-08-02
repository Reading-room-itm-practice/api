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
        public Task<ServiceResponse<IEnumerable<FriendDto>>> GetApprovedFriendRequests(int? id);
        public Task<ServiceResponse<IEnumerable<SentFriendRequestDto>>> GetSentFriendRequests(int? id);
        public Task<ServiceResponse<IEnumerable<ReceivedFriendRequestDto>>> GetReceivedFriendRequests(int? id);
        public Task<ServiceResponse> SendFriendRequest(SendFriendRequest request);
        public Task<ServiceResponse> RemoveFriendRequest(int id);
        public Task<ServiceResponse> AcceptOrDeclineFriendRequest(ApproveFriendRequest friendRequest, int friendRequestId);
    }
}
