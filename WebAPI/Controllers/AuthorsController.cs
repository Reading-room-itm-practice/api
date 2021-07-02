using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DTOs;
using WebAPI.Interfaces.Authors;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorCreator _creator;
        private readonly IAuthorGetter _getter;
        private readonly IAuthorUpdater _updater;
        private readonly IAuthorDeleter _deleter;

        public AuthorsController(IAuthorCreator creator, IAuthorGetter getter, IAuthorUpdater updater, IAuthorDeleter deleter)
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
            var authors = await _getter.GetAllAuthors();
            
            return Ok(authors);
        }

        [SwaggerOperation(Summary = "Retrieves a specific book author by unique id")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Show(int id)
        {
            var author = await _getter.GetAuthorById(id);
            
            return author == null ? NotFound() : Ok(author);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAuthorDto model) 
        {
            var author = await _creator.AddAuthor(model);

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
            await _deleter.DeleteAuthor(id);
            return Ok("Resource deleted");
        }
    }
}
