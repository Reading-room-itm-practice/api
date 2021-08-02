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
using System.Net.Http;
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
        private readonly IMapper _mapper;

        public PhotoService(IConfiguration configuration, IPhotoRepository photoRepository, ICrudService<Photo> crud, 
            IGetterService<Author> authorGetter, IGetterService<Book> bookGetter, UserManager<User> userManager, IMapper mapper)
        {
            _crud = crud;
            _photoRepository = photoRepository;
            _authorGetter = authorGetter;
            _bookGetter = bookGetter;
            _userManager = userManager;
            _mapper = mapper;

            PhotoSizeLimit = long.Parse(configuration["FileSizeLimit"]);
            AllowedFileExtensions = configuration["AllowedPhotoExtensions"].Split(", ").ToList();
            uploadsFolder = configuration["PhotoUploadsFolder"];
        }

        public async Task<ServiceResponse> GetPhotos(string typeId, PhotoTypes type)
        {
            if (int.TryParse(typeId, out int idInt))
            {
                if (type == PhotoTypes.AuthorPhoto)
                    return ServiceResponse<IEnumerable<PhotoDto>>.Success(_mapper.Map<IEnumerable<PhotoDto>>
                        (_photoRepository.GetAuthorPhotos(idInt)), "Author photos retrieved.");
                if (type == PhotoTypes.BookPhoto)
                    return ServiceResponse<IEnumerable<PhotoDto>>.Success(_mapper.Map<IEnumerable<PhotoDto>>
                        (_photoRepository.GetBookPhotos(idInt)), "Book photos retrieved.");
                return ServiceResponse.Error("Photos not found.", HttpStatusCode.NotFound);
            }

            if(Guid.TryParse(typeId, out Guid idGuid))
            {
                return ServiceResponse<PhotoDto>.Success(_mapper.Map<PhotoDto>
                        (await _photoRepository.GetUserPhotos(idGuid)), "User photo retrieved.");
            }

            return ServiceResponse.Error("Invalid Id.", HttpStatusCode.BadRequest);
        }

        public async Task<ServiceResponse> GetPhoto(int id)
        {
            var photo = (await _crud.GetById<PhotoDto>(id)).Content;
            if (photo != null && photo.Photo != null)
            {
                return ServiceResponse<PhotoDto>.Success(photo, "Photo retrieved.");
            }
            return ServiceResponse.Error("Photo not found.");
        }

        public async Task<ServiceResponse> DeletePhoto(int photoId)
        {
            if(await _photoRepository.DeletePhoto(photoId)) return ServiceResponse.Success("Photo deleted.");
            return ServiceResponse.Error("Photo not found", HttpStatusCode.NotFound);
        }

        public async Task<ServiceResponse> UploadPhoto(IFormFile image, string id, PhotoTypes type)
        {
            ServiceResponse validationResult = ValidatePhoto(image);
            if (!validationResult.SuccessStatus) return validationResult;
            if (!(await ItemExists(id, type))) 
                return ServiceResponse.Error("The item you're trying to upload an image for, doesn't exist.");

            string photoPath = await ProcessPhoto(image);
            if (photoPath == null) return ServiceResponse.Error("An error ocurred while uploading the image.");

            Photo photoRequest = null;
            if (type == PhotoTypes.AuthorPhoto)
                photoRequest = new AuthorPhoto() { AuthorId = int.Parse(id), Path = photoPath, PhotoType = type };
            else if (type == PhotoTypes.BookPhoto)
                photoRequest = new BookPhoto() { BookId = int.Parse(id), Path = photoPath, PhotoType = type };
            else if (type == PhotoTypes.ProfilePhoto)
            {
                if ((await _photoRepository.GetUserPhotos(Guid.Parse(id))) != null)
                    return ServiceResponse.Error("User already has a profile photo.", HttpStatusCode.BadRequest);
                photoRequest = new ProfilePhoto() { UserId = Guid.Parse(id), Path = photoPath, PhotoType = type };
            }
                
            var newPhoto = _mapper.Map<PhotoDto>(await _photoRepository.UploadPhoto(photoRequest));

            return ServiceResponse<PhotoDto>.Success(newPhoto, "Photo uploaded.", HttpStatusCode.Created);
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
            if (AllowedFileExtensions.All(ex => extension != ex)) return ServiceResponse.Error("Invalid file extension", 
                HttpStatusCode.BadRequest);

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
