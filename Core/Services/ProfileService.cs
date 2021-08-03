using AutoMapper;
using Core.DTOs;
using Core.Interfaces.Profile;
using Core.ServiceResponses;
using Storage.Interfaces;
using System;
using System.Threading.Tasks;

namespace Core.Services.Profile
{
    class ProfileService : IProfileService
    {
        private readonly IMapper _mapper;
        private readonly IProfileHelper _helper;

        public ProfileService(IMapper mapper, IProfileHelper helper)
        {
            _mapper = mapper;
            _helper = helper;
        }

        public async Task<ServiceResponse> GetProfile(Guid? id)
        {

            var profile = await _helper.GetUserProfile(id);
            var profileDto = _mapper.Map<UserProfileDto>(profile);

            return ServiceResponse<UserProfileDto>.Success(profileDto, "User profile retrieved" );
        }
    }
}
