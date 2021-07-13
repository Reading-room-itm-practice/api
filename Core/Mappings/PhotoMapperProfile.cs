using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DTOs;
using Storage.Models;
using Core.DTOs;
using Core.Requests;

namespace Core.Mappings
{
    public class PhotoMapperProfile : Profile
    {
        public PhotoMapperProfile()
        {
            CreateMap<Photo, PhotoDto>().ReverseMap();
            CreateMap<PhotoUploadRequest, Photo>().ReverseMap();
            CreateMap<PhotoUpdateRequest, Photo>().ReverseMap();
        }
    }
}
