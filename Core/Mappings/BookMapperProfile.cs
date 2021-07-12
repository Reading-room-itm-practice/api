using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Storage.Models;
using Core.DTOs;
using Core.Requests;

namespace Core.Mappings
{
    public class BookMapperProfile : Profile
    {
        public BookMapperProfile()
        {
            CreateMap<Book, BookDto>().ReverseMap();
            CreateMap<BookRequest, Book>().ReverseMap();
        }
    }
}
