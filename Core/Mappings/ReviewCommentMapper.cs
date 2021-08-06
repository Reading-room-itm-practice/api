using System;
using System.Linq;
using AutoMapper;
using Core.DTOs;
using Core.Interfaces;
using Core.Requests;
using Storage.Interfaces;
using Storage.Models;

namespace Core.Mappings
{
    class ReviewCommentMapper : Profile
    {
        public ReviewCommentMapper(IHelper loggedUserId)
        {
            CreateMap<ReviewCommentRequest, ReviewComment>().ReverseMap();
            CreateMap<ReviewComment, ReviewCommentDto>()
                .ForMember(dest => dest.LikesCount, opt
                    => opt.MapFrom(src => src.Likes.Count))
                .ForMember(x=>x.LoggedUserLiked, opt
                    =>opt.MapFrom(c=>c.Likes.Any(i => i.CreatorId == loggedUserId.Get())))
                .ReverseMap();
        }
    }
}
