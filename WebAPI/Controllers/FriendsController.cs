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

        [HttpGet("FriendRequest/{id}")]
        public async Task<ServiceResponse> ReceivedFriendRequest(int id)
        {
            return await _friendService.GetReceivedFriendRequest<FriendRequestDto>(id);
        }

        // retrieves received friend requests
        [HttpGet("FriendRequests")]
        public async Task<ServiceResponse> ReceivedFriendRequests()
        {
            return await _friendService.GetReceivedFriendRequests<FriendRequestDto>();
        }

        [HttpGet]
        public async Task<ServiceResponse> Friends()
        {
            return await _friendService.GetFriends();
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
        public async Task<ServiceResponse> Accept(int id)
        {
            return await _friendService.AcceptFriendRequest(id);
        }
    }
}
