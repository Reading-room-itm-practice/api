using Core.Interfaces;
using Core.Requests;
using Core.Response;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
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

        [SwaggerOperation(Summary = "Retrieves all approved friends")]
        [HttpGet("{userId?}")]
        public async Task<ServiceResponse> Friends(Guid? userId)
        {
            return await _friendService.GetApprovedFriendRequests(userId);
        }

        [SwaggerOperation(Summary = "Retrieves all or specyfic friend request")]
        [HttpGet("ReceivedFriendRequests/{friendRequestId?}")]
        public async Task<ServiceResponse> ReceivedFriendRequest(int? friendRequestId)
        {
            return await _friendService.GetReceivedFriendRequests(friendRequestId);
        }

        [SwaggerOperation(Summary = "Retrieves all or specyfic sent friend request")]
        [HttpGet("SentFriendRequests/{friendRequestId?}")]
        public async Task<ServiceResponse> SentFriendRequests(int? friendRequestId)
        {
            return await _friendService.GetSentFriendRequests(friendRequestId);
        }

        [SwaggerOperation(Summary = "Create friend request for a specyfic user")]
        [HttpPost("SendFriendRequest")]
        public async Task<ServiceResponse> Create(SendFriendRequest request)
        {
            return await _friendService.SendFriendRequest(request);
        }

        [SwaggerOperation(Summary = "Accept friend request")]
        [HttpPut("FriendRequests/Accept/{friendRequestId}")]
        public async Task<ServiceResponse> Accept(ApproveFriendRequest friendRequest, int friendRequestId)
        {
            return await _friendService.AcceptOrDeclineFriendRequest(friendRequest, friendRequestId);
        }

        [SwaggerOperation(Summary = "Delete friend request for specific user")]
        [HttpDelete("FriendRequests/Remove/{userId}")]
        public async Task<ServiceResponse> DeleteFriendRequest(Guid userId)
        {
            return await _friendService.RemoveFriendRequest(userId);
        }

    }
}
