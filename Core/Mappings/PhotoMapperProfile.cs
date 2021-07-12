using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DTOs;
using Storage.Models;

namespace WebAPI.Mappings
{
    public class PhotoMapperProfile : Profile
    {
        public PhotoMapperProfile()
        {
            CreateMap<Photo, PhotoResponseDto>().ReverseMap();
            CreateMap<PhotoRequestDto, Photo>().ReverseMap();
            CreateMap<PhotoUpdateDto, Photo>().ReverseMap();
        }
    }
}
