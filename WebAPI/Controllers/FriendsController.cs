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

        // TESTING
        [HttpGet("ReceivedFriendRequests")]
        public async Task<ServiceResponse> ReceivedFriendRequests()
        {
            return await _friendService.GetFriendRequests<FriendDto>(false, true, false);
        }

        [HttpGet("SentFriendRequests")]
        public async Task<ServiceResponse> SentFriendRequests()
        {
            return await _friendService.GetFriendRequests<FriendDto>(true, false, false);
        }

        [HttpGet]
        public async Task<ServiceResponse> Friends()
        {
            return await _friendService.GetFriendRequests<FriendDto>(true, true, true);
        }

        [HttpGet("FriendRequests/{id}")]
        public async Task<ServiceResponse> ReceivedFriendRequest(int id)
        {
            return await _friendService.GetReceivedFriendRequest<ReceivedFriendRequestDto>(id);
        }

        [HttpGet("{id}")]
        public async Task<ServiceResponse> Friend(Guid id)
        {
            return await _friendService.GetFriend(id);
        }

        [HttpPost("SendFriendRequest")]
        public async Task<ServiceResponse> Create(SendFriendRequest request)
        {
            return await _friendService.SendFriendRequest(request);
        }

        [HttpPut("FriendRequests/Accept/{id}")]
        public async Task<ServiceResponse> Accept(ApproveFriendRequest friendRequest, int id)
        {
            return await _friendService.AcceptOrDeclineFriendRequest(friendRequest, id);
        }

        [HttpDelete("FriendRequests/Remove/{id}")]
        public async Task<ServiceResponse> DeleteFriendRequest(int id)
        {
            return await _friendService.RemoveFriendRequest(id);
        }

    }
}
