using AutoMapper;
using Core.DTOs;
using Core.Requests;
using Storage.Models.Photos;
using System;

namespace Core.Mappings
{
    public class PhotoMapperProfile : Profile
    {
        public PhotoMapperProfile()
        {
            CreateMap<AuthorPhoto, PhotoDto>().AfterMap((src, dest) =>
            {
                dest.TypeId = src.AuthorId.ToString();
            });
            CreateMap<PhotoUploadRequest, AuthorPhoto>().AfterMap((src, dest) =>
            {
                dest.AuthorId = int.Parse(src.TypeId);
            });

            CreateMap<BookPhoto, PhotoDto>().AfterMap((src, dest) =>
            {
                dest.TypeId = src.BookId.ToString();
            });
            CreateMap<PhotoUploadRequest, BookPhoto>().AfterMap((src, dest) =>
            {
                dest.BookId = int.Parse(src.TypeId);
            });

            CreateMap<ProfilePhoto, PhotoDto>().AfterMap((src, dest) =>
            {
                dest.TypeId = src.UserId.ToString();
            });
            CreateMap<PhotoUploadRequest, ProfilePhoto>().AfterMap((src, dest) =>
            {
                dest.UserId = Guid.Parse(src.TypeId);
            });
        }
    }
}
