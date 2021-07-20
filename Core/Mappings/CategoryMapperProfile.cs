using AutoMapper;
using Core.DTOs;
using Core.Requests;
using Storage.Models;

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
