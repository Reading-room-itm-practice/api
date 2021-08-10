using AutoMapper;
using Core.DTOs;
using Core.Requests;
using Storage.Models;

namespace Core.Mappings
{
    class ReviewCommentMapper : Profile
    {
        public ReviewCommentMapper()
        {
            CreateMap<ReviewCommentRequest, ReviewComment>().ReverseMap();
            CreateMap<ReviewComment, ReviewCommentDto>()
                .ForMember(dest => dest.LikesCount, opt
                    => opt.MapFrom(src => src.Likes.Count))
                .ForMember(x=>x.IsLoggedUserLike, opt
                    => opt.Ignore()).ReverseMap();
        }
    }
}
