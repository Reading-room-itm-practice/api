using AutoMapper;
using Core.DTOs.Follows;
using Core.Requests.Follows;
using Storage.Models.Follows;

namespace Core.Mappings.Follows
{
    public class FollowMapperProfile : Profile
    {
        public FollowMapperProfile()
        {
            CreateMap<AuthorFollow, FollowDto>()
                .ForMember(dest => dest.Id, opt
                => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FollowableType, opt
                => opt.MapFrom(src => src.FollowableType.ToStringValue()))
                .ForMember(dest => dest.Name, opt
                => opt.MapFrom(src => src.Author.Name))
                .ForMember(dest => dest.PhotoUrl, opt
                => opt.MapFrom(src => src.Author.MainPhoto.Path));

            CreateMap<CategoryFollow, FollowDto>()
                .ForMember(dest => dest.Id, opt
                => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FollowableType, opt
                => opt.MapFrom(src => src.FollowableType.ToStringValue()))
                .ForMember(dest => dest.Name, opt
                => opt.MapFrom(src => src.Category.Name));

            CreateMap<UserFollow, UserFollowDto>()
                .ForMember(dest => dest.Id, opt
                => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FollowableType, opt
                => opt.MapFrom(src => src.FollowableType.ToStringValue()))
                .ForMember(dest => dest.Name, opt
                => opt.MapFrom(src => src.Following.UserName))
                .ForMember(dest => dest.PhotoUrl, opt
                => opt.MapFrom(src => src.Following.ProfilePhoto.Path)); 

            CreateMap<FollowRequest, AuthorFollow>()
                .ForMember(dest => dest.AuthorId, opt 
                => opt.MapFrom(src => src.FollowableId))
                .ForMember(dest => dest.CreatorId, opt
                =>opt.MapFrom(src => src.CreatorId));

            CreateMap<FollowRequest, CategoryFollow>()
                .ForMember(dest => dest.CategoryId, opt 
                => opt.MapFrom(src => src.FollowableId))
                .ForMember(dest => dest.CreatorId, opt
                => opt.MapFrom(src => src.CreatorId));

            CreateMap<UserFollowRequest, UserFollow>()
                .ForMember(dest => dest.FollowingId, opt
                => opt.MapFrom(src => src.FollowableId))
                .ForMember(dest => dest.CreatorId, opt
                => opt.MapFrom(src => src.CreatorId));
        }
    }
}
