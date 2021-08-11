using AutoMapper;
using Core.DTOs;

namespace Core.Mappings
{
    class UserProfileMapperProfile : Profile
    {
        public UserProfileMapperProfile()
        {
            CreateMap<UserProfile, UserProfileDto>().ReverseMap()
                .ForMember(d => d.FavouriteBooks, opt => opt.MapFrom(src => src.FavouriteBooks))
                .ForMember(d => d.ToReadBooks, opt => opt.MapFrom(src => src.ToReadBooks))
                .ForMember(d => d.ReadBooks, opt => opt.MapFrom(src => src.ReadBooks));

            CreateMap<ForeignUserProfile, ForeignUserProfileDto>().ReverseMap()
                .ForMember(d => d.FavouriteBooks, opt => opt.MapFrom(src => src.FavouriteBooks))
                .ForMember(d => d.ToReadBooks, opt => opt.MapFrom(src => src.ToReadBooks))
                .ForMember(d => d.ReadBooks, opt => opt.MapFrom(src => src.ReadBooks));
        }
    }
}
