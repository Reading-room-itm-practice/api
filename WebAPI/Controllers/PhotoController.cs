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
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly ICrudService<Photo> _crud;
        private readonly IPhotoService _photoService;

        public PhotoController(ICrudService<Photo> crud, IPhotoService photoService)
        {
            _crud = crud;
            _photoService = photoService;
        }

        [HttpGet("All")]
        public async Task<ServiceResponse> GetPhotos([FromQuery] PaginationFilter filter, int? book_id)
        {
            var route = Request.Path.Value;
            if (book_id != null)
            {
                return ServiceResponse<IEnumerable<PhotoDto>>.Success(_crud.GetAll<PhotoDto>(filter, route).Result.Content.Data.Where(p => p.BookId == book_id));
            }

            return await _crud.GetAll<PhotoDto>(filter, route);
        }

        [HttpGet("{id:int}")]
        public async Task<ServiceResponse> GetPhoto(int id)
        {
            var result = await _crud.GetById<PhotoDto>(id);
            if (result.Content == null)
            {
                return ServiceResponse.Error("Photo not found.", HttpStatusCode.NotFound);
            } 

            return result;
        }

        [HttpPost()]
        public async Task<ServiceResponse> Upload(IFormFile image, int bookId)
        {
            try
            {
                var result = await _photoService.UploadPhoto(image, bookId);
                return result;
            }
            catch (DbUpdateException e)
            {
                return ServiceResponse.Error(e.InnerException.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ServiceResponse> Edit(int id, PhotoUpdateRequest photo_bookId)
        {
            try
            {
                await _crud.Update(photo_bookId, id);
                return ServiceResponse.Success("Image updated");
            }
            catch (DbUpdateException e)
            {
                return ServiceResponse.Error(e.InnerException.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ServiceResponse> Delete(int id)
        {
            try
            {
                var result = await _photoService.DeletePhoto(id);

                return ServiceResponse.Success("Image deleted.");
            }
            catch (NotFoundException e)
            {
                return ServiceResponse.Error(e.Message, HttpStatusCode.BadRequest);
            }
            catch (DbUpdateException e)
            {
                return ServiceResponse.Error(e.InnerException.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception e)
            {
                if (e.InnerException.GetType() == typeof(NotFoundException))
                {
                    return ServiceResponse.Error("Image not found", HttpStatusCode.NotFound);
                }

                return ServiceResponse.Error(e.Message);
            }
        }
    }
}
