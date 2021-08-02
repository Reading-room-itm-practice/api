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

        [HttpGet]
        public async Task<ServiceResponse> Friends()
        {
            return await _friendService.GetApprovedFriendRequests(null);
        }

        [HttpGet("ReceivedFriendRequests")]
        public async Task<ServiceResponse> ReceivedFriendRequests()
        {
            return await _friendService.GetReceivedFriendRequests(null);
        }

        [HttpGet("SentFriendRequests")]
        public async Task<ServiceResponse> SentFriendRequests()
        {
            return await _friendService.GetSentFriendRequests(null);
        }

        [HttpGet("{id}")]
        public async Task<ServiceResponse> Friend(int id)
        {
            return await _friendService.GetApprovedFriendRequests(id);
        }

        [HttpGet("ReceivedFriendRequests/{id}")]
        public async Task<ServiceResponse> ReceivedFriendRequest(int id)
        {
            return await _friendService.GetReceivedFriendRequests(id);
        }

        [HttpGet("SentFriendRequests/{id}")]
        public async Task<ServiceResponse> SentFriendRequests(int id)
        {
            return await _friendService.GetSentFriendRequests(id);
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
