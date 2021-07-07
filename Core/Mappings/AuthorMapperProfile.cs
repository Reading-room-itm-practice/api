using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DTOs;
using Core.Models;

namespace Core.Mappings
{
    public class AuthorMapperProfile : Profile
    {
        public AuthorMapperProfile()
        {
            CreateMap<Author, AuthorResponseDto>().ReverseMap();
            CreateMap<AuthorRequestDto, Author>().ReverseMap();
        }
    }
}
