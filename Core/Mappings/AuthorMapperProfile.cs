using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DTOs;
using Storage.Models;

namespace Core.Mappings
{
    public class AuthorMapperProfile : Profile
    {
        public AuthorMapperProfile()
        {
            CreateMap<Author, AuthorDto>().ReverseMap();
            CreateMap<AuthorRequest, Author>().ReverseMap();
        }
    }
}
