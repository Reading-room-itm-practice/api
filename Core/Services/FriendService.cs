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

        // TODO - fix function so it returns different DTO's
        public async Task<ServiceResponse<IEnumerable<FriendDto>>> GetFriendRequests<FriendRequestDto>(bool sent, bool received, bool approved)
        {
            Guid userId = _user.GetUserId();
            IEnumerable<FriendRequest> friendRequests = Enumerable.Empty<FriendRequest>();

            if (approved)
            {
                friendRequests = await _repository.GetSentAndReceivedFriendRequests(userId);
            } 
            else if(sent)
            {
                friendRequests = await _repository.GetSentFriendRequests(userId);
            }
            else
            {
                friendRequests = await _repository.GetReceivedFriendRequests(userId);
            }

            var friendList = new List<FriendDto>();
            foreach (var friendRequest in friendRequests)
            {
                var user = friendRequest.ToId == userId ? friendRequest.Creator : friendRequest.To;
                friendList.Add(new FriendDto { Id = user.Id, UserName = user.UserName });
            }
            return ServiceResponse<IEnumerable<FriendDto>>.Success(friendList, "Retrieved list with resources.");
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
            var friends = await _repository.FindByConditions(x => x.Approved && (x.ToId == request.ToId || x.CreatorId == request.ToId));
            if(friends.Any())
            {
                return ServiceResponse.Error("Friend has already been added.");
            }
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
            var friendRequests = await _repository.GetSentAndReceivedFriendRequests(userId);
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
            var friendRequests = await _repository.GetSentAndReceivedFriendRequests(id);
            var friendRequest = _mapper.Map<FriendRequest>(friendRequests.FirstOrDefault());
            FriendDto friend = null;

            if (friendRequest != null)
            {
                var user = friendRequest.ToId == id ? friendRequest.Creator : friendRequest.To;
                friend = new FriendDto { Id = user.Id, UserName = user.UserName };
            }
            return ServiceResponse<FriendDto>.Success(friend, "Retrieved resource.");
        }

        public async Task<ServiceResponse> RemoveFriendRequest(int id)
        {
            Guid userId = _user.GetUserId();
            var model = await _repository.FindByConditions(fr => fr.Id == id && (fr.CreatorId == userId || fr.ToId == userId));

            if (model.FirstOrDefault() == null)
            {
                return ServiceResponse.Error("Entity does not exist or cannot be removed by current user.");
            }
            await _repository.Delete(model.FirstOrDefault());
            return ServiceResponse.Success("Friend Request has been removed.");
        }

        public async Task<ServiceResponse> AcceptOrDeclineFriendRequest(ApproveFriendRequest friendRequest, int friendRequestId)
        {
            Guid userId = _user.GetUserId();
            var model = await _repository.FindByConditions(fr => fr.Id == friendRequestId && !fr.Approved && fr.ToId == userId);
            if (!model.Any())
            {
                return ServiceResponse.Error("Not Found.");
            }
            var retrievedRequest = _mapper.Map<FriendRequest>(model.FirstOrDefault());
            var updateModel = _mapper.Map(friendRequest, retrievedRequest);

            await _repository.Edit(updateModel);
            if(retrievedRequest.Approved)
            {
                return ServiceResponse.Success("Friend has been added.");
            }
            await _repository.Delete(retrievedRequest);
            return ServiceResponse.Success("Rejected Friend Request.");
        }
    }
}
