using AutoMapper;
using Core.Requests;
using Storage.Models;
using WebAPI.DTOs;

namespace Core.Mappings
{
    public class CategoryMapperProfile : Profile
    {
        public CategoryMapperProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<CategoryRequest, Category>().ReverseMap();
        }
    }
}
