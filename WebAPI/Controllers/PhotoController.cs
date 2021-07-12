using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Core.Common;
using WebAPI.DTOs;
using Core.Exceptions;
using Storage.Models;
using Core.Interfaces;

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
        public async Task<ActionResult> GetPhotos(int? book_id)
        {
            if (book_id != null)
            {
                var result = _crud.GetAll<PhotoResponseDto>().Result.Where(p => p.BookId == book_id);
                return Ok(result);
            }
            return Ok(await _crud.GetAll<PhotoResponseDto>());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetPhoto(int id)
        {
            var result = await _crud.GetById<PhotoResponseDto>(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost()]
        public async Task<ActionResult> Upload(IFormFile image, int bookId)
        {
            try
            {
                var result = await _photoService.UploadPhoto(image, bookId);
                return StatusCode(result.Key, result.Value);
            }
            catch (DbUpdateException e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Edit(int id, PhotoUpdateDto photo_bookId)
        {
            try
            {
                await _crud.Update(photo_bookId, id);
                return Ok();
            }
            catch (DbUpdateException e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var result = await _photoService.DeletePhoto(id);
                return StatusCode(result.Key, result.Value);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
                throw;
            }
            catch(DbUpdateException e)
            {
                return BadRequest(e.InnerException.Message);
            }
            catch(Exception e)
            {
                if (e.InnerException.GetType() == typeof(NotFoundException)) return NotFound("Image not found");
                return BadRequest(e.Message);
            }
        }
    }
}
