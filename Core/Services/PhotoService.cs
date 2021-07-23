using AutoMapper;
using Core.DTOs;
using Core.Interfaces;
using Core.Requests;
using Core.ServiceResponses;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Core.Services
{
    public class PhotoService : IPhotoService
    {
        public readonly List<string> AllowedFileExtensions;
        public readonly long PhotoSizeLimit;
        public readonly string uploadsFolder;

        private readonly ICrudService<Photo> _crud;

        public PhotoService(IConfiguration configuration, ICrudService<Photo> crud)
        {
            _crud = crud;

            PhotoSizeLimit = long.Parse(configuration["FileSizeLimit"]);
            AllowedFileExtensions = configuration["AllowedPhotoExtensions"].Split(", ").ToList();
            uploadsFolder = configuration["PhotoUploadsFolder"];
        }

        public async Task<ServiceResponse> UploadPhoto(IFormFile image, int bookId)
        {
            ServiceResponse result = ValidatePhoto(image);
            if (!result.SuccessStatus) return result;

            return await ProcessPhoto(image, bookId);
        }

        public async Task<ServiceResponse> DeletePhoto(int id)
        {
            var photoToDelete = await _crud.GetById<PhotoDto>(id);
            await _crud.Delete(id);
            File.Delete(photoToDelete.Content.Path);
            return  ServiceResponse.Success();
        }

        private ServiceResponse ValidatePhoto(IFormFile image)
        {
            if (image == null) return ServiceResponse.Error("No image.", HttpStatusCode.BadRequest);
            if (image.Length > PhotoSizeLimit) return ServiceResponse.Error("File is too large.", HttpStatusCode.BadRequest);
            var extension = "." + image.FileName.Split('.')[image.FileName.Split('.').Length - 1];
            if (AllowedFileExtensions.All(ex => extension != ex)) return ServiceResponse.Error("Invalid file extension", HttpStatusCode.BadRequest);

            return  ServiceResponse.Success();
        }

        private async Task<ServiceResponse> ProcessPhoto(IFormFile image, int bookId)
        {
            try
            {
                string uniqueFileName = Guid.NewGuid().ToString() + ".jpeg";
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                var newPhoto = await _crud.Create<PhotoDto>(new PhotoUploadRequest { Path = filePath, BookId = bookId });
                using (var stream = File.Create(filePath))
                {
                    await image.CopyToAsync(stream);
                }

                return  ServiceResponse<PhotoDto>.Success(newPhoto.Content, "Image uploaded." );
            }
            catch (Exception e)
            {
                if (e.InnerException != null) return ServiceResponse.Error(e.Message + " Inner Exception: " + e.InnerException.Message, HttpStatusCode.BadRequest );

                return  ServiceResponse.Error( e.Message, HttpStatusCode.BadRequest);
            }
        }
    }
}
