using AutoMapper;
using Core.DTOs;
using Core.Requests;
using Microsoft.AspNetCore.Identity;
using Storage.Identity;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Mappings
{
    class FriendRequestMapperProfile : Profile
    {
        public FriendRequestMapperProfile()
        {
            CreateMap<FriendRequest, FriendRequestDto>().ReverseMap();
            CreateMap<AcceptFriendRequest, FriendRequest>().ReverseMap();
            CreateMap<SendFriendRequest, FriendRequest>().ReverseMap();

            CreateMap<FriendDto, User>().ReverseMap();
            CreateMap<FriendDto, FriendRequest>().ReverseMap();
        }
    }
}
