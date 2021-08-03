using Core.DTOs;
using Core.Interfaces;
using Core.Requests;
using Core.ServiceResponses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendsController : ControllerBase
    {
        private readonly IFriendService _friendService;
        public FriendsController(IFriendService friendService)
        {
            _friendService = friendService;
        }

        [HttpGet("{userId?}")]
        public async Task<ServiceResponse> Friends(Guid? userId)
        {
            return await _friendService.GetApprovedFriendRequests(userId);
        }

        [HttpGet("ReceivedFriendRequests/{friendRequestId?}")]
        public async Task<ServiceResponse> ReceivedFriendRequest(int? friendRequestId)
        {
            return await _friendService.GetReceivedFriendRequests(friendRequestId);
        }

        [HttpGet("SentFriendRequests/{friendRequestId?}")]
        public async Task<ServiceResponse> SentFriendRequests(int? friendRequestId)
        {
            return await _friendService.GetSentFriendRequests(friendRequestId);
        }

        [HttpPost("SendFriendRequest")]
        public async Task<ServiceResponse> Create(SendFriendRequest request)
        {
            return await _friendService.SendFriendRequest(request);
        }

        [HttpPut("FriendRequests/Accept/{id}")]
        public async Task<ServiceResponse> Accept(ApproveFriendRequest friendRequest, int friendRequestId)
        {
            return await _friendService.AcceptOrDeclineFriendRequest(friendRequest, friendRequestId);
        }

        [HttpDelete("FriendRequests/Remove/{userId}")]
        public async Task<ServiceResponse> DeleteFriendRequest(Guid userId)
        {
            return await _friendService.RemoveFriendRequest(userId);
        }

    }
}
