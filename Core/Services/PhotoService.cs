using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using WebAPI.DTOs;
using Storage.Models;
using Microsoft.AspNetCore.Hosting;
using Core.Interfaces;
using Core.DTOs;
using AutoMapper;
using Core.Requests;
using Core.ServiceResponses;

namespace Core.Services
{
    public class PhotoService : IPhotoService
    {
        public readonly List<string> AllowedFileExtensions;
        public readonly long PhotoSizeLimit;
        public readonly string uploadsFolder;

        private readonly ICrudService<Photo> _crud;
        private readonly ICrudService<Book> _bookCrud;
        private readonly IMapper _mapper;
        private readonly IConfiguration configuration;

        public PhotoService(IConfiguration configuration, ICrudService<Photo> crud, ICrudService<Book> bookCrud, IMapper mapper)
        {
            this.configuration = configuration;
            _crud = crud;
            _bookCrud = bookCrud;
            _mapper = mapper;

            PhotoSizeLimit = long.Parse(configuration["FileSizeLimit"]);
            AllowedFileExtensions = configuration["AllowedPhotoExtensions"].Split(", ").ToList();
            uploadsFolder = configuration["PhotoUploadsFolder"];
        }

        public async Task<ServiceResponse> UploadPhoto(IFormFile image, int bookId)
        {
            if (image == null) return new ErrorResponse() { Message = "No image.", StatusCode = System.Net.HttpStatusCode.BadRequest };
            if (image.Length > PhotoSizeLimit) return new ErrorResponse() { Message = "File is too large.", StatusCode = System.Net.HttpStatusCode.BadRequest };
            var extension = "." + image.FileName.Split('.')[image.FileName.Split('.').Length - 1];
            if (AllowedFileExtensions.All(ex => extension != ex)) return new ErrorResponse() { Message = "Invalid file extension", StatusCode = System.Net.HttpStatusCode.BadRequest };
            string uniqueFileName = Guid.NewGuid().ToString() + ".jpeg";
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            var newPhoto = await _crud.Create<PhotoDto>(new PhotoUploadRequest { Path = filePath, BookId = bookId });
            using (var stream = System.IO.File.Create(filePath))
            {
                await image.CopyToAsync(stream);
            }

            return new SuccessResponse<PhotoDto>() { Content = newPhoto };
        }

        public async Task<ServiceResponse> DeletePhoto(int id)
        {
            var photoToDelete = await _crud.GetById<PhotoDto>(id);
            await _crud.Delete(id);
            System.IO.File.Delete(photoToDelete.Path);
            return new SuccessResponse();
        }
    }
}
