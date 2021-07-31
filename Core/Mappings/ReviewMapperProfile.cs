using AutoMapper;
using Core.DTOs;
using Core.Requests;
using Storage.Models;
using System.Collections.Generic;

namespace Core.Mappings
{
    class ReviewMapperProfile : Profile
    {
        public ReviewMapperProfile()
        {
            CreateMap<Review, ReviewDto>().ReverseMap();
            CreateMap<DataDto<IEnumerable<Review>>, DataDto<IEnumerable<ReviewDto>>>().ReverseMap();

            CreateMap<ReviewRequest, Review>().ReverseMap();
        }
    }
}
