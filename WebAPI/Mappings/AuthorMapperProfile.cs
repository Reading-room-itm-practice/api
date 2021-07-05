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
            CreateMap<Author, AuthorResponseDto>().ReverseMap();
            CreateMap<AuthorRequestDto, Author>().ReverseMap();
            CreateMap<AuthorRequestDto, Author>().ReverseMap();
        }
    }
}
