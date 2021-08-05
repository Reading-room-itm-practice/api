using AutoMapper;
using Core.DTOs.Follows;
using Core.Requests.Follows;
using Storage.Models.Follows;

namespace Core.Mappings
{
    class FollowersMapperProfile : Profile
    {
        public FollowersMapperProfile()
        {
            CreateMap<AuthorFollow, FollowerDto>()
                .ForMember(dest => dest.Id, opt
                    => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FollowerId, opt
                    => opt.MapFrom(src => src.CreatorId))
                .ForMember(dest => dest.Name, opt
                    => opt.MapFrom(src => src.Creator.UserName))
                .ForMember(dest => dest.PhotoUrl, opt
                    => opt.MapFrom(src => src.Creator.ProfilePhoto.Path));

            CreateMap<CategoryFollow, FollowerDto>()
                .ForMember(dest => dest.Id, opt
                    => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FollowerId, opt
                    => opt.MapFrom(src => src.CreatorId))
                .ForMember(dest => dest.Name, opt
                    => opt.MapFrom(src => src.Creator.UserName));

            CreateMap<UserFollow, FollowerDto>()
                .ForMember(dest => dest.Id, opt
                    => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FollowerId, opt
                    => opt.MapFrom(src => src.CreatorId))
                .ForMember(dest => dest.Name, opt
                    => opt.MapFrom(src => src.Creator.UserName))
                .ForMember(dest => dest.PhotoUrl, opt
                    => opt.MapFrom(src => src.Creator.ProfilePhoto.Path));
        }
    }
}
