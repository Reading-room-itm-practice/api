using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DTOs;
using WebAPI.Models;

namespace WebAPI.Mappings
{
    public class AuthorMapperProfile : Profile
    {
        public AuthorMapperProfile()
        {
            CreateMap<Author, AuthorDto>().ReverseMap();
            CreateMap<CreateAuthorDto, Author>().ReverseMap();
        }
    }
}
