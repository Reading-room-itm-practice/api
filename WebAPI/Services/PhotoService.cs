using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Interfaces;
using System.IO;
using WebAPI.DTOs;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly IConfiguration configuration;

        public readonly List<string> AllowedFileExtensions;
        public readonly long PhotoSizeLimit;

        private readonly ICrudService<Photo> _crud;
        public PhotoService(IWebHostEnvironment hostingEnvironment, IConfiguration configuration, ICrudService<Photo> crud)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.configuration = configuration;
            _crud = crud;

            AllowedFileExtensions = new string(configuration.GetValue<string>("AllowedPhotoExtensions")).Split(", ").ToList();
            PhotoSizeLimit = configuration.GetValue<long>("FileSizeLimit");
        }

        public async Task<KeyValuePair<int,string>> UploadPhoto(IFormFile image, int? bookId)
        {
            if (image == null) return new KeyValuePair<int, string>(StatusCodes.Status400BadRequest, "No image.");
            if (image.Length > PhotoSizeLimit) return new KeyValuePair<int, string>(StatusCodes.Status400BadRequest, "File is too large.");//BadRequest("File is too large");
            var extension = "." + image.FileName.Split('.')[image.FileName.Split('.').Length - 1];
            if (AllowedFileExtensions.All(ex => extension != ex)) return new KeyValuePair<int, string>(StatusCodes.Status400BadRequest, "Invalid file extension");

            string uniqueFileName = Guid.NewGuid().ToString() + ".jpeg";
            string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "Photos");
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var stream = System.IO.File.Create(filePath))
            {
                await image.CopyToAsync(stream);
            }

            var newPhoto = await _crud.Create<PhotoResponseDto>(new PhotoRequestDto(filePath, bookId));
            return new KeyValuePair<int, string>(StatusCodes.Status201Created, "Image added.");
            //Created($"api/authors/{newPhoto.Id}", newPhoto);
        }
        public async Task<KeyValuePair<int, string>> DeletePhoto(int id)
        {
            var photoToDelete = await _crud.GetById<PhotoResponseDto>(id);
            await _crud.Delete(id);
            System.IO.File.Delete(photoToDelete.Path);
            return new KeyValuePair<int, string>(StatusCodes.Status200OK, "Photo deleted.");
        }
    }
}
