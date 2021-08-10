using AutoMapper;
using Core.DTOs;
using Core.Requests;
using Storage.Models;

namespace Core.Mappings
{
    class ReviewMapperProfile : Profile
    {
        public ReviewMapperProfile()
        {
            CreateMap<ReviewRequest, Review>().ReverseMap();
            CreateMap<Review, ReviewDto>().ForMember(dest => dest.LikesCount, opt
                    => opt.MapFrom(src => src.Likes.Count))
                .ForMember(x => x.IsLoggedUserLike, opt
                    => opt.Ignore()).ReverseMap();
        }
    }
}
