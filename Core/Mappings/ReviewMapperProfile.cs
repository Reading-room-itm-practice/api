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
            CreateMap<Review, ReviewDto>().ReverseMap();
            CreateMap<ReviewRequest, Review>().ReverseMap();
            CreateMap<DataDto<ReviewDto>, DataDto<Review>>().ReverseMap();
        }
    }
}
