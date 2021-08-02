using AutoMapper;
using Core.DTOs;
using Core.Requests;
using Storage.Models.Photos;
using System;
using System.IO;
using System.Net.Http;

namespace Core.Mappings
{
    public class PhotoMapperProfile : Profile
    {
        public PhotoMapperProfile()
        {
            CreateMap<AuthorPhoto, PhotoDto>().AfterMap((src, dest) =>
            {
                dest.TypeId = src.AuthorId.ToString();
                dest.Photo = new StreamContent(new FileStream(src.Path, FileMode.Open));
            });
            CreateMap<PhotoUploadRequest, AuthorPhoto>().AfterMap((src, dest) =>
            {
                dest.AuthorId = int.Parse(src.TypeId);
            });

            CreateMap<BookPhoto, PhotoDto>().AfterMap((src, dest) =>
            {
                dest.TypeId = src.BookId.ToString();
                dest.Photo = new StreamContent(new FileStream(src.Path, FileMode.Open));
            });
            CreateMap<PhotoUploadRequest, BookPhoto>().AfterMap((src, dest) =>
            {
                dest.BookId = int.Parse(src.TypeId);
            });

            CreateMap<ProfilePhoto, PhotoDto>().AfterMap((src, dest) =>
            {
                dest.TypeId = src.UserId.ToString();
                dest.Photo = new StreamContent(new FileStream(src.Path, FileMode.Open));
            });
            CreateMap<PhotoUploadRequest, ProfilePhoto>().AfterMap((src, dest) =>
            {
                dest.UserId = Guid.Parse(src.TypeId);
            });
        }
    }
}
