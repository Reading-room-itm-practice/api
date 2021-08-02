using AutoMapper;
using Core.DTOs;
using Core.Requests;
using Core.Services.Search;
using Storage.Models;
using System.Collections.Generic;

namespace Core.Mappings
{
    class ReviewMapperProfile : Profile
    {
        public ReviewMapperProfile()
        {
            CreateMap<Review, ReviewDto>().ReverseMap();
            CreateMap<ExtendedData<IEnumerable<Review>>, ExtendedData<IEnumerable<ReviewDto>>>().ReverseMap();

            CreateMap<ReviewRequest, Review>().ReverseMap();
        }
    }
}
