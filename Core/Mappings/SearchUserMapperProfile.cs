using AutoMapper;
using Core.DTOs;
using Storage.Identity;

namespace Core.Mappings

{
    class SearchUserMapperProfile : Profile
    {
        public SearchUserMapperProfile()
        {
            CreateMap<User, UserSearchDto>().ReverseMap();
        }
    }
}
