using AutoMapper;
using Core.DTOs;
using Core.Services.Search;
using Storage.Identity;
using Storage.Models;

namespace Core.Mappings

{
    class SearchMapperProfile : Profile
    {
        public SearchMapperProfile()
        {
            CreateMap<User, UserSearchDto>().ReverseMap();
            CreateMap<SearchAllDto, SearchAll>().ReverseMap();
            CreateMap<DataDto<Author>, DataDto<AuthorDto>>().ReverseMap();
            CreateMap<DataDto<Book>, DataDto<BookDto>>().ReverseMap();
            CreateMap<DataDto<Category>, DataDto<CategoryDto>>().ReverseMap();
            CreateMap<DataDto<User>, DataDto<UserSearchDto>>().ReverseMap();
            CreateMap<DataDto<SearchAllDto>, DataDto<SearchAll>>().ReverseMap();
        }
    }
}
