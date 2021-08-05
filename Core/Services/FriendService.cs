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
        private readonly IFriendRepository _friendRepository;

        private readonly ILoggedUserProvider _loggedUser;
        private readonly IMapper _mapper;
        public FriendService(
            IFriendRepository repository,
            ILoggedUserProvider loggedUser,
            IMapper mapper,
            ICreatorService<FriendRequest> creator,
            IGetterService<FriendRequest> getter,
            IUpdaterService<FriendRequest> updater,
            IDeleterService<FriendRequest> deleter)
            : base(creator, getter, updater, deleter)
        {
            _friendRepository = repository;
            _mapper = mapper;
            _loggedUser = loggedUser;
        }

        public async Task<ServiceResponse<IEnumerable<FriendDto>>> GetApprovedFriendRequests(Guid? userId)
        {
            var searchedUserId = userId ?? _loggedUser.GetUserId();
            var friendRequests = await _friendRepository.GetSentAndReceivedFriendRequests(searchedUserId);
            var friendList = new List<FriendDto>();

            foreach (var friendRequest in friendRequests)
            {
                var user = friendRequest.ToId == userId ? friendRequest.To : friendRequest.Creator;
                friendList.Add(new FriendDto { Id = user.Id, UserName = user.UserName });
            }

            return ServiceResponse<IEnumerable<FriendDto>>.Success(friendList, "Retrieved list with resources.");
        }

        public async Task<ServiceResponse<IEnumerable<SentFriendRequestDto>>> GetSentFriendRequests(int? friendRequestId)
        {
            Guid userId = _loggedUser.GetUserId();
            var friendRequests = await _friendRepository.GetSentFriendRequests(userId);
            var friendList = new List<SentFriendRequestDto>();

            var foundFriendRequest = friendRequests.FirstOrDefault(fr => fr.Id == friendRequestId);
            if (foundFriendRequest != null)
            {
                var user = foundFriendRequest.To;
                friendList.Add(new SentFriendRequestDto { Id = foundFriendRequest.Id, ToId = user.Id, UserName = user.UserName });
            }
            else if (friendRequestId == null)
            {
                foreach (var friendRequest in friendRequests)
                {
                    var user = friendRequest.ToId == userId ? friendRequest.Creator : friendRequest.To;
                    friendList.Add(new SentFriendRequestDto { Id = friendRequest.Id, ToId = user.Id, UserName = user.UserName });
                }
            }

            return ServiceResponse<IEnumerable<SentFriendRequestDto>>.Success(friendList, "Retrieved list with resources.");
        }

        public async Task<ServiceResponse<IEnumerable<ReceivedFriendRequestDto>>> GetReceivedFriendRequests(int? friendRequestId)
        {
            Guid userId = _loggedUser.GetUserId();
            var friendRequests = await _friendRepository.GetReceivedFriendRequests(userId);
            var friendList = new List<ReceivedFriendRequestDto>();

            var foundFriendRequest = friendRequests.FirstOrDefault(fr => fr.Id == friendRequestId);
            if (foundFriendRequest != null)
            {
                var user = foundFriendRequest.Creator;
                friendList.Add(new ReceivedFriendRequestDto { Id = foundFriendRequest.Id, CreatorId = user.Id, UserName = user.UserName });
            }
            else if (friendRequestId == null)
            {
                foreach (var friendRequest in friendRequests)
                {
                    var user = friendRequest.ToId == userId ? friendRequest.Creator : friendRequest.To;
                    friendList.Add(new ReceivedFriendRequestDto { Id = friendRequest.Id, CreatorId = user.Id, UserName = user.UserName });
                }
            }

            return ServiceResponse<IEnumerable<ReceivedFriendRequestDto>>.Success(friendList, "Retrieved list with resources.");
        }

        public async Task<ServiceResponse> SendFriendRequest(SendFriendRequest request)
        {
            Guid userId = _loggedUser.GetUserId();
            var sentFriendRequests = await _friendRepository.GetSentFriendRequests(userId);
            var receivedFriendRequests = await _friendRepository.GetReceivedFriendRequests(userId);
            var friends = await _friendRepository.FindByConditions(x => x.Approved 
            && ((x.ToId == request.ToId && x.CreatorId == userId) 
            || (x.CreatorId == request.ToId && x.ToId == userId)));
            
            if (friends.Any())
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
                    await _friendRepository.Edit(item);

                    return ServiceResponse.Success("Friend has been added.");
                }
            }

            if (userId == model.ToId)
            {
                return ServiceResponse.Error("Unable to send friend request to user with specified id.");
            }

            await _friendRepository.Create(model);
            return ServiceResponse.Success("Friend Request has been created.");
        }

        public async Task<ServiceResponse> RemoveFriendRequest(Guid userId)
        {
            Guid LoggedUserId = _loggedUser.GetUserId();
            var model = await _friendRepository.FindByConditions(fr => 
            (fr.CreatorId == LoggedUserId && fr.ToId == userId) 
            || (fr.CreatorId == userId && fr.ToId == LoggedUserId));

            if (model.FirstOrDefault() == null)
            {
                return ServiceResponse.Error("Entity does not exist or cannot be removed by current user.");
            }
            await _friendRepository.Delete(model.FirstOrDefault());
            return ServiceResponse.Success("Friend Request has been removed.");
        }

        public async Task<ServiceResponse> AcceptOrDeclineFriendRequest(ApproveFriendRequest friendRequest, int friendRequestId)
        {
            Guid userId = _loggedUser.GetUserId();
            var model = await _friendRepository.FindByConditions(fr => fr.Id == friendRequestId && !fr.Approved && fr.ToId == userId);
            if (!model.Any())
            {
                return ServiceResponse.Error("Not Found.");
            }
            var retrievedRequest = _mapper.Map<FriendRequest>(model.FirstOrDefault());
            var updateModel = _mapper.Map(friendRequest, retrievedRequest);

            await _friendRepository.Edit(updateModel);
            if (retrievedRequest.Approved)
            {
                return ServiceResponse.Success("Friend has been added.");
            }
            await _friendRepository.Delete(retrievedRequest);
            return ServiceResponse.Success("Rejected Friend Request.");
        }

        public async Task<bool> IsFriend(Guid? userId)
        {
            Guid LoggedUserId = _loggedUser.GetUserId();
            var friendRequests = await _friendRepository.GetSentAndReceivedFriendRequests(LoggedUserId);

            var result = friendRequests.Where(fr => (fr.CreatorId == userId && fr.ToId == LoggedUserId) || (fr.CreatorId == LoggedUserId && fr.ToId == userId));
            return result.Any();
        }
    }
}