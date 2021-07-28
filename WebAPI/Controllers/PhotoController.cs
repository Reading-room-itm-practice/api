﻿using Core.DTOs;
using Core.Exceptions;
using Core.Interfaces;
using Core.Requests;
using Core.ServiceResponses;
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
        public async Task<ServiceResponse> GetPhotos(string typeId, PhotoTypes? type)
        {
            if (typeId != null && type != null)
            {
                return ServiceResponse<IEnumerable<PhotoDto>>.Success(_crud.GetAll<PhotoDto>().Result.Content
                    .Where(p => p.TypeId == typeId && p.PhotoType == type));
            }

            return await _crud.GetAll<PhotoDto>();
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
