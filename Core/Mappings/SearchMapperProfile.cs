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
            
            CreateMap<SearchAll, SearchAllDto>().ReverseMap();
            CreateMap<DataDto<SearchAll>, DataDto<SearchAllDto>>().ReverseMap();

            CreateMap<IEnumerable<Author>, AuthorDto>().ReverseMap();
            CreateMap<DataDto<IEnumerable<Author>>, DataDto<IEnumerable<AuthorDto>>>().ReverseMap();

            CreateMap<IEnumerable<Book>, BookDto>().ReverseMap();
            CreateMap<DataDto<IEnumerable<Book>>, DataDto<IEnumerable<BookDto>>>().ReverseMap();

            CreateMap<IEnumerable<Category>, CategoryDto>().ReverseMap();
            CreateMap<DataDto<IEnumerable<Category>>, DataDto<IEnumerable<CategoryDto>>>().ReverseMap();

            CreateMap<User, UserSearchDto>().ReverseMap();
            CreateMap<IEnumerable<User>, UserSearchDto>().ReverseMap();
            CreateMap<DataDto<IEnumerable<User>>, DataDto<IEnumerable<UserSearchDto>>>().ReverseMap();
        }
    }
}
