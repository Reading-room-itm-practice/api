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
            CreateMap<Review, ReviewDto>().ReverseMap();
            CreateMap<ReviewRequest, Review>().ReverseMap();
        }
    }
}
