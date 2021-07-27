using AutoMapper;
using Core.DTOs;
using Core.Interfaces;
using Core.Requests;
using Core.ServiceResponses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Storage.Identity;
using Storage.Models;
using Storage.Models.Photos;
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
        private readonly IPhotoRepository _photoRepository;
        private readonly IGetterService<Author> _authorGetter;
        private readonly IGetterService<Book> _bookGetter;
        private readonly UserManager<User> _userManager;

        public PhotoService(IConfiguration configuration, IPhotoRepository photoRepository, ICrudService<Photo> crud, 
            IGetterService<Author> authorGetter, IGetterService<Book> bookGetter, UserManager<User> userManager)
        {
            _crud = crud;
            _photoRepository = photoRepository;
            _authorGetter = authorGetter;
            _bookGetter = bookGetter;
            _userManager = userManager;

            PhotoSizeLimit = long.Parse(configuration["FileSizeLimit"]);
            AllowedFileExtensions = configuration["AllowedPhotoExtensions"].Split(", ").ToList();
            uploadsFolder = configuration["PhotoUploadsFolder"];
        }
        public async Task<ServiceResponse> DeletePhoto(int id)
        {
            var photoToDelete = await _crud.GetById<PhotoDto>(id);
            await _crud.Delete(id);
            File.Delete(photoToDelete.Content.Path);
            return ServiceResponse.Success();
        }
        public async Task<ServiceResponse> UpdatePhoto(int id, PhotoUpdateRequest photo, PhotoTypes type)
        {
            if ((await _crud.GetById<PhotoDto>(id)).Content == null)
                return ServiceResponse.Error("Photo doesn't exist.");

            await _photoRepository.UpdatePhoto(id, photo, type);
            return ServiceResponse.Success("Photo updated.");
        }

        public async Task<ServiceResponse> UploadPhoto(IFormFile image, string id, PhotoTypes type)
        {
            ServiceResponse validationResult = ValidatePhoto(image);
            if (!validationResult.SuccessStatus) return validationResult;
            if (!(await ItemExists(id, type))) 
                return ServiceResponse.Error("The item you're trying to upload an image for, doesn't exist.");

            string photoPath = await ProcessPhoto(image);
            if (photoPath == null) return ServiceResponse.Error("An error ocurred while uploading the image.");

            var uploadResult = await _photoRepository.UploadPhoto(new PhotoUploadRequest() { TypeId = id, Path = photoPath }, type);

            return ServiceResponse<PhotoDto>.Success(uploadResult, "Photo uploaded.", HttpStatusCode.Created);
        }
               
        private async Task<bool> ItemExists(string id, PhotoTypes type)
        {
            switch(type)
            {
                case (PhotoTypes.AuthorPhoto):
                    return (await _authorGetter.GetById<AuthorDto>(int.Parse(id))).Content != null;
                case (PhotoTypes.BookPhoto):
                    return (await _bookGetter.GetById<BookDto>(int.Parse(id))).Content != null;
                case (PhotoTypes.ProfilePhoto):
                    return (await _userManager.FindByIdAsync(id)) != null;
                default:
                    return false;
            }
        }

        private ServiceResponse ValidatePhoto(IFormFile image)
        {
            if (image == null) return ServiceResponse.Error("No image.", HttpStatusCode.BadRequest);
            if (image.Length > PhotoSizeLimit) return ServiceResponse.Error("File is too large.", HttpStatusCode.BadRequest);
            var extension = "." + image.FileName.Split('.')[image.FileName.Split('.').Length - 1];
            if (AllowedFileExtensions.All(ex => extension != ex)) return ServiceResponse.Error("Invalid file extension", HttpStatusCode.BadRequest);

            return  ServiceResponse.Success();
        }

        private async Task<string> ProcessPhoto(IFormFile image)
        {
            string uniqueFileName = Guid.NewGuid().ToString() + ".jpeg";
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var stream = File.Create(filePath))
            {
                await image.CopyToAsync(stream);
            }
            return filePath;
        }
    }
}
