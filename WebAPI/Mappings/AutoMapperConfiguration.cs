using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DTOs;
using WebAPI.Models;

namespace WebAPI.Mappings
{
    public class AutoMapperConfiguration
    {

             public static IMapper Initialize()
               => new MapperConfiguration(cfg =>
               {
                   cfg.CreateMap<Author, AuthorDto>();
                   cfg.CreateMap<CreateAuthorDto, Author>();
                   cfg.CreateMap<Category, CategoryDTO>();
                   cfg.CreateMap<CreateCategoryDTO, Category>();
                   cfg.CreateMap<EditCategoryDTO, Category>();
               })
               .CreateMapper();
     }
}
