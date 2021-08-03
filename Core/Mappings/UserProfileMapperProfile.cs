using AutoMapper;
using Core.DTOs;
using Storage.Identity;

namespace Core.Mappings

{
    class UserProfileMapperProfile : Profile
    {
        public UserProfileMapperProfile()
        {
            CreateMap<UserProfile, UserProfileDto>().ReverseMap();
        }
    }
}
