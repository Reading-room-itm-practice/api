using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DTOs;
using WebAPI.Models;

namespace WebAPI.Mappings
{
    public class CategoryMapperProfile : Profile
    {
        public CategoryMapperProfile()
        {
            CreateMap<Category, CategoryResponseDto>().ReverseMap();
            CreateMap<CategoryRequestDto, Category>().ReverseMap();
        }
    }
}
