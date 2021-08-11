using Core.Interfaces;
using Core.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Storage.Models.Photos;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly IPhotoService _photoService;

        public PhotoController(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        [SwaggerOperation(Summary = "Retrieves all photos")]
        [HttpGet("All")]
        public async Task<ServiceResponse> GetPhotos([Required] string typeId, [Required] PhotoTypes type)
        {
            return await _photoService.GetPhotos(typeId, type);
        }

        [SwaggerOperation(Summary = "Retrieves specific photo by unique id")]
        [HttpGet("{photoId:int}")]
        public async Task<ServiceResponse> GetPhoto(int photoId)
        {
            return await _photoService.GetPhoto(photoId);
        }

        [SwaggerOperation(Summary = "Uploads new image for specyfic target")]
        [HttpPost("Upload")]
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

        [SwaggerOperation(Summary = "Delete specific photo by unique id")]
        [HttpDelete("Delete/{photoId:int}")]
        public async Task<ServiceResponse> Delete(int photoId)
        {
            return await _photoService.DeletePhoto(photoId);
        }
    }
}
