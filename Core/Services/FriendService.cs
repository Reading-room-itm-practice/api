using AutoMapper;
using Core.Common;
using Core.DTOs;
using Core.Interfaces;
using Core.Requests;
using Core.ServiceResponses;
using Microsoft.AspNetCore.Identity;
using Storage.Identity;
using Storage.Interfaces;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class FriendService : CrudService<FriendRequest>, IFriendService
    {
        private readonly IFriendRepository _repository;
        
        private readonly ILoggedUserProvider _user;
        private readonly IMapper _mapper;
        public FriendService(
            IFriendRepository repository,
            ILoggedUserProvider user,
            IMapper mapper,
            ICreatorService<FriendRequest> creator,
            IGetterService<FriendRequest> getter,
            IUpdaterService<FriendRequest> updater,
            IDeleterService<FriendRequest> deleter)
            : base(creator, getter, updater, deleter)
        {
            _repository = repository;
            _mapper = mapper;
            _user = user;
        }

        public async Task<ServiceResponse<IEnumerable<FriendRequestDto>>> GetReceivedFriendRequests<FriendRequestDto>()
        {
            var models = await _repository.FindByConditions(x => !x.Approved && x.ToId == _user.GetUserId());
            var responseModels = _mapper.Map<IEnumerable<FriendRequestDto>>(models);

            return ServiceResponse<IEnumerable<FriendRequestDto>>.Success(responseModels, "Retrieved list with resources.");
        }

        public async Task<ServiceResponse<FriendRequestDto>> GetReceivedFriendRequest<FriendRequestDto>(int id)
        {
            var model = await _repository.FindByConditions(x => !x.Approved && x.ToId == _user.GetUserId() && x.Id == id);
            var responseModel = _mapper.Map<FriendRequestDto>(model.FirstOrDefault());

            return ServiceResponse<FriendRequestDto>.Success(responseModel, "Retrieved resource.");
        }

        public async Task<ServiceResponse> SendFriendRequest(SendFriendRequest request)
        {
            Guid userId = _user.GetUserId();
            var sentFriendRequests = await _repository.FindByConditions(x => !x.Approved && x.CreatorId == userId);
            var receivedFriendRequests = await _repository.FindByConditions(x => !x.Approved && x.ToId == userId);

            var model = _mapper.Map<FriendRequest>(request);

            foreach (var item in sentFriendRequests)
            {
                if (model.ToId == item.ToId)
                {
                    return ServiceResponse.Error("Unable to send friend request to user with specified id.");
                }
            }

            foreach (var item in receivedFriendRequests)
            {
                if (model.ToId == item.CreatorId)
                {
                    item.Approved = true;
                    await _repository.Edit(item);

                    return ServiceResponse.Success("Friend has been added.");
                }
            }

            if (userId == model.ToId)
            {
                return ServiceResponse.Error("Unable to send friend request to user with specified id.");
            }

            await _repository.Create(model);
            return ServiceResponse.Success("Friend Request has been created.");
        }

        public async Task<ServiceResponse<List<FriendDto>>> GetFriends()
        {
            Guid userId = _user.GetUserId();
            var friendRequests = await _repository.GetApprovedSentAndReceivedFriendRequests(userId);
            var friendList = new List<FriendDto>();

            if (friendRequests != null)
            {
                foreach (var friendRequest in friendRequests)
                {
                    var user = friendRequest.ToId == userId ? friendRequest.Creator : friendRequest.To;
                    friendList.Add(new FriendDto { Id = user.Id, UserName = user.UserName });
                }
            }

            return ServiceResponse<List<FriendDto>>.Success(friendList, "Retrieved list with resources.");
        }

        public async Task<ServiceResponse<FriendDto>> GetFriend(Guid id)
        {
            var friendRequest = await _repository.GetApprovedSentAndReceivedFriendRequest(id);
            FriendDto friend = null;

            if (friendRequest != null)
            {
                var user = friendRequest.ToId == id ? friendRequest.Creator : friendRequest.To;
                friend = new FriendDto { Id = user.Id, UserName = user.UserName };
            }
            return ServiceResponse<FriendDto>.Success(friend, "Retrieved resource.");
        }

        public async Task<ServiceResponse> RemoveReceivedFriendRequest(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse> RemoveSentFriendRequest(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse> RemoveFriend(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse> AcceptFriendRequest(int friendRequestId)
        {
            Guid userId = _user.GetUserId();
            var model = await _repository.FindByConditions(fr => fr.Id == friendRequestId && !fr.Approved && fr.ToId == userId);
            var retrievedRequest = _mapper.Map<FriendRequest>(model);

            retrievedRequest.Approved = true;
            await _repository.Edit(retrievedRequest);

            return ServiceResponse.Success("Friend request was accepted.");
        }
    }
}
