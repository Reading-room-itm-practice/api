using Core.DTOs;
using Core.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Storage.Identity;
using Storage.Interfaces;
using Storage.Models.Photos;
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
        private readonly IPhotoService _photoService;
        private readonly UserManager<User> _userManager;

        public ProfileHelper(IProfileRepository profileRepository, IFriendService friendService,
            ILoggedUserProvider loggedUserProvider, IPhotoRepository photoRepository, IPhotoService photoService, UserManager<User> userManager)
        {
            _profileRepository = profileRepository;
            _friendService = friendService;
            _loggedUserProvider = loggedUserProvider;
            _photoRepository = photoRepository;
            _photoService = photoService;
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

        public async Task<ServiceResponse> EditPhoto(IFormFile image)
        {
            var userId = _loggedUserProvider.GetUserId();
            var oldImage = await _photoRepository.GetUserPhoto(userId);
            if (oldImage != null)
            {
                await _photoService.EditPhoto(oldImage, image);
                return ServiceResponse.Success("Photo edited");
            }

            return await _photoService.UploadPhoto(image, userId.ToString(), PhotoTypes.ProfilePhoto);
        }
    }
}
