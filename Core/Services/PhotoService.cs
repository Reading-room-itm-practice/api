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

namespace Core.Services
{
    public class PhotoService : IPhotoService
    {
        
        private readonly IConfiguration configuration;

        public readonly List<string> AllowedFileExtensions;
        public readonly long PhotoSizeLimit;
        public readonly string uploadsFolder;

        private readonly ICrudService<Photo> _crud;
        private readonly ICrudService<Book> _bookCrud;
        private readonly IMapper _mapper;
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

        public async Task<KeyValuePair<int,string>> UploadPhoto(IFormFile image, int bookId)
        {
            if (image == null) return new KeyValuePair<int, string>(StatusCodes.Status400BadRequest, "No image.");
            if (image.Length > PhotoSizeLimit) return new KeyValuePair<int, string>(StatusCodes.Status400BadRequest, "File is too large.");
            var extension = "." + image.FileName.Split('.')[image.FileName.Split('.').Length - 1];
            if (AllowedFileExtensions.All(ex => extension != ex)) return new KeyValuePair<int, string>(StatusCodes.Status400BadRequest, "Invalid file extension");

            string uniqueFileName = Guid.NewGuid().ToString() + ".jpeg";
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            var newPhoto = await _crud.Create<PhotoResponseDto>(new PhotoRequestDto(filePath, bookId));
            using (var stream = System.IO.File.Create(filePath))
            {
                await image.CopyToAsync(stream);
            }

            return new KeyValuePair<int, string>(StatusCodes.Status201Created, "Image added.");
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
