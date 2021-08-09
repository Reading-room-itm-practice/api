using Core.DTOs;
using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Storage.Identity;
using Storage.Interfaces;
using System;
using System.Threading.Tasks;

namespace Core.Interfaces.Profile
{
    public class ProfileHelper : IProfileHelper
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IFriendService _friendService;
        private readonly ILoggedUserProvider _loggedUserProvider;
        private readonly IPhotoRepository _photoRepository;
        private readonly UserManager<User> _userManager;

        public ProfileHelper(IProfileRepository profileRepository, IFriendService friendService, ILoggedUserProvider loggedUserProvider, IPhotoRepository photoRepository, UserManager<User> userManager)
        {
            _profileRepository = profileRepository;
            _friendService = friendService;
            _loggedUserProvider = loggedUserProvider;
            _photoRepository = photoRepository;
            _userManager = userManager;
        }

        public async Task<UserProfile> GetUserProfile(Guid? id)
        {
            var userId = id ?? _loggedUserProvider.GetUserId();
            bool isFriend = await _friendService.IsFriend(userId);
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                return new UserProfile();

            var profile = _profileRepository.GetProfile(user, isFriend);
            profile.Photo = await _photoRepository.GetUserPhoto(user.Id);
            profile.FriendList = _friendService.GetApprovedFriendRequests(null).Result.Content;

            return profile;
        }
    }
}
