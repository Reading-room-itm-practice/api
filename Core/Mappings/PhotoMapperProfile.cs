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
            //CreateMap<AuthorPhoto, BookPhoto>().AfterMap((src, dest) =>
            //{
            //    dest.BookId = src.AuthorId;
            //    src.AuthorId = 0;
            //    //dest.AuthorId = int.Parse(src.TypeId);
            //});

            CreateMap<AuthorPhoto, BookPhoto>().ReverseMap();
            CreateMap<Photo, BookPhoto>().ReverseMap();
            CreateMap<AuthorPhoto, Photo>().ReverseMap();


            CreateMap<AuthorPhoto, PhotoDto>().ReverseMap();
            CreateMap<PhotoUploadRequest, AuthorPhoto>().AfterMap((src, dest) =>
            {
                dest.AuthorId = int.Parse(src.TypeId);
            });
            CreateMap<PhotoUpdateRequest, AuthorPhoto>().AfterMap((src, dest) =>
            {
                dest.AuthorId = int.Parse(src.TypeId);
            });

            CreateMap<BookPhoto, PhotoDto>().ReverseMap();
            CreateMap<PhotoUploadRequest, BookPhoto>().AfterMap((src, dest) =>
            {
                dest.BookId = int.Parse(src.TypeId);
            });
            CreateMap<PhotoUpdateRequest, BookPhoto>().AfterMap((src, dest) =>
            {
                dest.BookId = int.Parse(src.TypeId);
            });

            CreateMap<ProfilePhoto, PhotoDto>().ReverseMap();
            CreateMap<PhotoUploadRequest, ProfilePhoto>().AfterMap((src, dest) =>
            {
                dest.UserId = Guid.Parse(src.TypeId);
            });
            CreateMap<PhotoUpdateRequest, ProfilePhoto>().AfterMap((src, dest) =>
            {
                dest.UserId = Guid.Parse(src.TypeId);
            });
        }
    }
}
