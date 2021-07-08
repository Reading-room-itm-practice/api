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
using WebAPI.Common;
using WebAPI.DTOs;
using WebAPI.Exceptions;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly ICrudService<Photo> _crud;
        private readonly IPhotoService _photoService;

        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly IConfiguration configuration;

        public readonly List<string> AllowedFileExtensions;
        public readonly long PhotoSizeLimit;

        public PhotoController(IWebHostEnvironment hostingEnvironment, IConfiguration configuration, ICrudService<Photo> crud, IPhotoService photoService)
        {
            _crud = crud;
            _photoService = photoService;
            this.hostingEnvironment = hostingEnvironment;
            this.configuration = configuration;

            AllowedFileExtensions = new string(configuration.GetValue<string>("AllowedPhotoExtensions")).Split(", ").ToList();
            PhotoSizeLimit = configuration.GetValue<long>("FileSizeLimit");
        }

        [HttpGet("All")]
        public async Task<ActionResult> GetPhotos(int? book_id)
        {
            if (book_id != null)
            {
                var result = await _crud.GetAll<PhotoResponseDto>();
                return Ok(result.Where(p => p.BookId == book_id));
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
        public async Task<ActionResult> Upload(IFormFile image, int? bookId)
        {
            try
            {
                var result = await _photoService.UploadPhoto(image, bookId);
                return StatusCode(result.Key, result.Value);

                //if (image == null) return BadRequest("No image.");
                //if (image.Length > PhotoSizeLimit) return BadRequest("File is too large");
                //var extension = "." + image.FileName.Split('.')[image.FileName.Split('.').Length - 1];
                //if (AllowedFileExtensions.All(ex => extension != ex)) return BadRequest("Invalid file extension");

                //string uniqueFileName = Guid.NewGuid().ToString() + ".jpeg";
                //string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "Photos");
                //string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                //using (var stream = System.IO.File.Create(filePath))
                //{
                //    await image.CopyToAsync(stream);
                //}

                //var newPhoto = await _crud.Create<PhotoResponseDto>(new PhotoRequestDto(filePath, bookId));
                //return Created($"api/authors/{newPhoto.Id}", newPhoto);
            }
            catch (DbUpdateException e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Edit(int id, PhotoUpdateDto photo)
        {
            try
            {
                await _crud.Update(photo, id);
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
                var result = _photoService.DeletePhoto(id);
                return StatusCode(result.Result.Key, result.Result.Value);
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
