using Core.DTOs;
using Core.Exceptions;
using Core.Interfaces;
using Core.Requests;
using Core.Response;
using Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Storage.Models;
using System;
using System.Collections.Generic;
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
        public async Task<ServiceResponse> GetPhotos([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var photos = (await _crud.GetAll<PhotoDto>(filter, route));

            return new SuccessResponse<PagedResponse<IEnumerable<PhotoDto>>>() { Message = "Photos retrieved.", Content = photos };
        }

        [HttpGet("{id:int}")]
        public async Task<ServiceResponse> GetPhoto(int id)
        {
            var result = await _crud.GetById<PhotoDto>(id);
            if (result == null) return new ErrorResponse() { Message = "Photo not found.", StatusCode = System.Net.HttpStatusCode.NotFound };
            return new SuccessResponse<PhotoDto>() { Content = result };
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
                return new ErrorResponse() { Message = e.InnerException.Message, StatusCode = HttpStatusCode.BadRequest };
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ServiceResponse> Edit(int id, PhotoUpdateRequest photo_bookId)
        {
            try
            {
                await _crud.Update(photo_bookId, id);
                return new SuccessResponse() { Message = "Image updated" };
            }
            catch (DbUpdateException e)
            {
                return new ErrorResponse() { Message = e.InnerException.Message, StatusCode = HttpStatusCode.BadRequest };
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ServiceResponse> Delete(int id)
        {
            try
            {
                var result = await _photoService.DeletePhoto(id);
                return new SuccessResponse() { Message = "Image deleted." };
            }
            catch (NotFoundException e)
            {
                return new ErrorResponse() { Message = e.Message, StatusCode = HttpStatusCode.BadRequest };

            }
            catch (DbUpdateException e)
            {
                return new ErrorResponse() { Message = e.InnerException.Message, StatusCode = HttpStatusCode.BadRequest };
            }
            catch (Exception e)
            {
                if (e.InnerException.GetType() == typeof(NotFoundException)) return new ErrorResponse()
                { Message = "Image not found", StatusCode = HttpStatusCode.NotFound };

                return new ErrorResponse() { Message = e.Message };
            }
        }
    }
}
