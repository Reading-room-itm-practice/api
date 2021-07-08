using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DTOs;
using WebAPI.Models;

namespace WebAPI.Mappings
{
    public class BookMapperProfile : Profile
    {
        public BookMapperProfile()
        {
            CreateMap<Book, BookResponseDto>().ReverseMap();
            CreateMap<BookRequestDto, Book>().ReverseMap();
        }
    }
}
