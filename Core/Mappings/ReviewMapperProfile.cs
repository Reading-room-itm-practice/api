using AutoMapper;
using Core.DTOs;
using Core.Requests;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Mappings
{
    class ReviewMapperProfile : Profile
    {
        public ReviewMapperProfile()
        {
            CreateMap<ReviewRequest, Review>().ReverseMap();
            CreateMap<ReviewDto, Review>();
            CreateMap<Review, ReviewDto>().AfterMap((src, dest) =>
            {
                dest.CreatorUserName = src.Creator.UserName;
            });
        }
    }
}
