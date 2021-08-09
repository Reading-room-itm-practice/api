using AutoMapper;
using Core.DTOs;
using Core.Interfaces;
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
        private readonly List<string> _allowedFileExtensions;
        private readonly long _photoSizeLimit;
        private readonly string _uploadsFolder;

        private readonly IPhotoRepository _photoRepository;
        private readonly IGetterService<Author> _authorGetter;
        private readonly IGetterService<Book> _bookGetter;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public PhotoService(IConfiguration configuration, IPhotoRepository photoRepository, IGetterService<Author> authorGetter, 
            IGetterService<Book> bookGetter, UserManager<User> userManager, IMapper mapper)
        {
            _photoRepository = photoRepository;
            _authorGetter = authorGetter;
            _bookGetter = bookGetter;
            _userManager = userManager;
            _mapper = mapper;

            _photoSizeLimit = long.Parse(configuration["FileSizeLimit"]);
            _allowedFileExtensions = configuration["AllowedPhotoExtensions"].Split(", ").ToList();
            _uploadsFolder = configuration["PhotoUploadsFolder"];
        }

        public async Task<ServiceResponse> GetPhotos(string typeId, PhotoTypes type)
        {
            if (!ValidateId(typeId, type)) return ServiceResponse.Error("Invalid Id.", HttpStatusCode.BadRequest);

            if (type == PhotoTypes.AuthorPhoto)return ServiceResponse<IEnumerable<PhotoDto>>.Success(_mapper.Map<IEnumerable<PhotoDto>>
                (await _photoRepository.GetAuthorPhotos(int.Parse(typeId))), "Author photos retrieved.");

            if (type == PhotoTypes.BookPhoto) return ServiceResponse<IEnumerable<PhotoDto>>.Success(_mapper.Map<IEnumerable<PhotoDto>>
                (await _photoRepository.GetBookPhotos(int.Parse(typeId))), "Book photos retrieved.");

            if (type == PhotoTypes.ProfilePhoto) return ServiceResponse<PhotoDto>.Success(_mapper.Map<PhotoDto>
                (await _photoRepository.GetUserPhoto(Guid.Parse(typeId))), "User photo retrieved.");

            return ServiceResponse.Error("Photos not found.", HttpStatusCode.NotFound);
        }

        public async Task<ServiceResponse> GetPhoto(int id)
        {
            var photo = _mapper.Map<PhotoDto>(await _photoRepository.GetPhoto(id));
            if (photo != null && photo.Content != null)
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
            if (!ValidateId(id, type)) return ServiceResponse.Error("Invalid Id.", HttpStatusCode.BadRequest);
            ServiceResponse validationResult = ValidatePhoto(image);
            if (!validationResult.SuccessStatus) return validationResult;
            if (!(await ItemExists(id, type))) 
                return ServiceResponse.Error("The item you're trying to upload an image for, doesn't exist.", HttpStatusCode.NotFound);

            string photoPath = await ProcessPhoto(image);
            if (photoPath == null) return ServiceResponse.Error("An error ocurred while uploading the image.");

            Photo photoRequest = null;
            if (type == PhotoTypes.AuthorPhoto)
                photoRequest = new AuthorPhoto() { AuthorId = int.Parse(id), Path = photoPath, PhotoType = type };
            else if (type == PhotoTypes.BookPhoto)
                photoRequest = new BookPhoto() { BookId = int.Parse(id), Path = photoPath, PhotoType = type };
            else if (type == PhotoTypes.ProfilePhoto)
            {
                var idGuid = Guid.Parse(id);
                if ((await _photoRepository.GetUserPhoto(idGuid) != null))
                    return ServiceResponse.Error("User already has a profile photo.", HttpStatusCode.BadRequest);
                photoRequest = new ProfilePhoto() { UserId = idGuid, Path = photoPath, PhotoType = type };
            }
                
            var newPhoto = _mapper.Map<PhotoDto>(await _photoRepository.UploadPhoto(photoRequest));
            return ServiceResponse<PhotoDto>.Success(newPhoto, "Photo uploaded.", HttpStatusCode.Created);
        }
               
        private bool ValidateId(string id, PhotoTypes type)
        {
            return (type == PhotoTypes.AuthorPhoto || type == PhotoTypes.BookPhoto) ? 
                int.TryParse(id, out int idInt) : Guid.TryParse(id, out Guid guid);
        }

        private async Task<bool> ItemExists(string id, PhotoTypes type)
        {
            if(type == PhotoTypes.AuthorPhoto) return (await _authorGetter.GetById<AuthorDto>(int.Parse(id))).Content != null;
            if(type == PhotoTypes.BookPhoto) return (await _bookGetter.GetById<BookDto>(int.Parse(id))).Content != null;
            if(type == PhotoTypes.ProfilePhoto) return (await _userManager.FindByIdAsync(id)) != null;
            return false;
        }

        private ServiceResponse ValidatePhoto(IFormFile image)
        {
            if (image == null) return ServiceResponse.Error("No image.", HttpStatusCode.BadRequest);
            if (image.Length > _photoSizeLimit) return ServiceResponse.Error("File is too large.", HttpStatusCode.BadRequest);
            var extension = image.FileName.Substring(image.FileName.LastIndexOf('.'));
            if (_allowedFileExtensions.All(ex => extension != ex)) return ServiceResponse.Error("Invalid file extension", 
                HttpStatusCode.BadRequest);

            return ServiceResponse.Success();
        }

        private async Task<string> ProcessPhoto(IFormFile image)
        {
            string uniqueFileName = Guid.NewGuid().ToString() + ".jpeg";
            Directory.CreateDirectory(_uploadsFolder);
            string filePath = Path.Combine(_uploadsFolder, uniqueFileName);
            using (var stream = File.Create(filePath))
            {
                await image.CopyToAsync(stream);
            }
            return filePath;
        }
    }
}
