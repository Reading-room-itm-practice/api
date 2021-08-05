using Core.DTOs;
using Core.Exceptions;
using Core.Interfaces;
using Core.Requests;
using Core.Response;
using Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Storage.Models.Photos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly IPhotoService _photoService;
        private readonly IGettterPaginationService _getPaged;

        public PhotoController(IPhotoService photoService, IGettterPaginationService getPaged)
        {
            _photoService = photoService;
            _getPaged = getPaged;
        }

        [HttpGet("All")]
        public async Task<ServiceResponse> GetPhotos([Required] string typeId, [Required] PhotoTypes type)
        {
            return await _photoService.GetPhotos(typeId, type);
        }

        [HttpGet("{photoId:int}")]
        public async Task<ServiceResponse> GetPhoto(int photoId)
        {
            return await _photoService.GetPhoto(photoId);
        }

        [HttpPost()]
        public async Task<ServiceResponse> Upload(IFormFile image, string id, PhotoTypes type)
        {
            try
            {
                var result = await _photoService.UploadPhoto(image, id, type);
                return result;
            }
            catch (DbUpdateException e)
            {
                return ServiceResponse.Error(e.InnerException.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpDelete("{photoId:int}")]
        public async Task<ServiceResponse> Delete(int photoId)
        {
            return await _photoService.DeletePhoto(photoId);
        }
    }
}
