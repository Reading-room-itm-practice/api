using AutoMapper;
using Core.DTOs;
using Core.Requests;
using Storage.Models.Photos;

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
