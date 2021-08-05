using AutoMapper;
using Core.DTOs;
using Core.Requests;
using Storage.Models.Photos;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Core.Mappings
{
    public class PhotoMapperProfile : Profile
    {
        public PhotoMapperProfile()
        {
            CreateMap<AuthorPhoto, PhotoDto>().AfterMap((src, dest) =>
            {
                dest.TypeId = src.AuthorId.ToString();
                dest.Content = new StreamContent(new FileStream(src.Path, FileMode.Open));
                dest.Content.Headers.ContentType = new MediaTypeHeaderValue($"image/{src.Path.Substring(src.Path.LastIndexOf('.')+1)}");
            });
            CreateMap<PhotoUploadRequest, AuthorPhoto>().AfterMap((src, dest) =>
            {
                dest.AuthorId = int.Parse(src.TypeId);
            });

            CreateMap<BookPhoto, PhotoDto>().AfterMap((src, dest) =>
            {
                dest.TypeId = src.BookId.ToString();
                dest.Content = new StreamContent(new FileStream(src.Path, FileMode.Open));
                dest.Content.Headers.ContentType = new MediaTypeHeaderValue($"image/{src.Path.Substring(src.Path.LastIndexOf('.')+1)}");
            });
            CreateMap<PhotoUploadRequest, BookPhoto>().AfterMap((src, dest) =>
            {
                dest.BookId = int.Parse(src.TypeId);
            });

            CreateMap<ProfilePhoto, PhotoDto>().AfterMap((src, dest) =>
            {
                dest.TypeId = src.UserId.ToString();
                dest.Content = new StreamContent(new FileStream(src.Path, FileMode.Open));
                dest.Content.Headers.ContentType = new MediaTypeHeaderValue($"image/{src.Path.Substring(src.Path.LastIndexOf('.')+1)}");
            });
            CreateMap<PhotoUploadRequest, ProfilePhoto>().AfterMap((src, dest) =>
            {
                dest.UserId = Guid.Parse(src.TypeId);
            });
        }
    }
}
