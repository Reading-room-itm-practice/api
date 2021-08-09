using AutoMapper;
using Core.DTOs;
using Core.Services.Search;
using Storage.Identity;
using Storage.Models;
using System.Collections.Generic;

namespace Core.Mappings

{
    class SearchMapperProfile : Profile
    {
        public SearchMapperProfile()
        {

            CreateMap<AllData, SearchAllDto>().ReverseMap();
            CreateMap<ExtendedData<AllData>, ExtendedData<SearchAllDto>>().ReverseMap();

            CreateMap<IEnumerable<Author>, AuthorDto>().ReverseMap();
            CreateMap<ExtendedData<IEnumerable<Author>>, ExtendedData<IEnumerable<AuthorDto>>>().ReverseMap();

            CreateMap<IEnumerable<Book>, BookDto>().ReverseMap();
            CreateMap<ExtendedData<IEnumerable<Book>>, ExtendedData<IEnumerable<BookDto>>>().ReverseMap();

            CreateMap<IEnumerable<Category>, CategoryDto>().ReverseMap();
            CreateMap<ExtendedData<IEnumerable<Category>>, ExtendedData<IEnumerable<CategoryDto>>>().ReverseMap();

            CreateMap<User, UserSearchDto>().ReverseMap();
            CreateMap<IEnumerable<User>, UserSearchDto>().ReverseMap();
            CreateMap<ExtendedData<IEnumerable<User>>, ExtendedData<IEnumerable<UserSearchDto>>>().ReverseMap();
        }
    }
}
