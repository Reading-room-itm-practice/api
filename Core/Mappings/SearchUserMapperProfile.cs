using AutoMapper;
using Core.DTOs;
using Storage.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
