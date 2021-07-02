using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DTOs;
using WebAPI.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using WebAPI.Services;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly ICreator<Author> _creator;
        private readonly IGetter<Author> _getter;
        private readonly IUpdater<Author> _updater;
        private readonly IDeleter<Author> _deleter;

        public AuthorsController(ICreator<Author> creator, IGetter<Author> getter, IUpdater<Author> updater, IDeleter<Author> deleter)
        {
            _creator = creator;
            _getter = getter;
            _updater = updater;
            _deleter = deleter;
        }

        [SwaggerOperation(Summary = "Retrieves all book authors")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var authors = await _getter.GetAll<AuthorDto>();
            
            return Ok(authors);
        }

        [SwaggerOperation(Summary = "Retrieves a specific book author by unique id")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Show(int id)
        {
            var author = await _getter.GetById<AuthorDto>(id);
            
            return author == null ? NotFound() : Ok(author);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAuthorDto model) 
        {
            var author = await _creator.Create<AuthorDto>(model);

            return Created($"api/authors/{author.Id}", author);
        }

        [SwaggerOperation(Summary = "Update a book author by unique id")]
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, UpdateAuthorDto updateModel)
        {
            await _updater.UpdateAuthor(updateModel, id);
            return Ok("Resource updated");
        }

        [SwaggerOperation(Summary = "Delete a book author by unique id")]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _deleter.Delete(id);
            return Ok("Resource deleted");
        }
    }
}
