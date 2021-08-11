using Core.DTOs;
using Core.Requests;
using Core.Response;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IFriendService : ICrudService<FriendRequest>
    {
        public Task<ServiceResponse<IEnumerable<FriendDto>>> GetApprovedFriendRequests(Guid? userId);
        public Task<ServiceResponse<IEnumerable<SentFriendRequestDto>>> GetSentFriendRequests(int? friendRequestId);
        public Task<ServiceResponse<IEnumerable<ReceivedFriendRequestDto>>> GetReceivedFriendRequests(int? friendRequestId);
        public Task<ServiceResponse> SendFriendRequest(SendFriendRequest request);
        public Task<ServiceResponse> RemoveFriendRequest(Guid userId);
        public Task<ServiceResponse> AcceptOrDeclineFriendRequest(ApproveFriendRequest friendRequest, int friendRequestId);
        public Task<bool> IsFriend(Guid? userId);
    }
}
