using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DTOs;
using Core.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using Core.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly ICreatorService<Author> _creator;
        private readonly IGetterService<Author> _getter;
        private readonly IUpdaterService<Author> _updater;
        private readonly IDeleterService<Author> _deleter;

        public AuthorsController(ICreatorService<Author> creator, IGetterService<Author> getter, IUpdaterService<Author> updater, IDeleterService<Author> deleter)
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
            var authors = await _getter.GetAll<AuthorResponseDto>();
            
            return Ok(authors);
        }

        [SwaggerOperation(Summary = "Retrieves a specific book author by unique id")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Show(int id)
        {
            var author = await _getter.GetById<AuthorResponseDto>(id);
            
            return author == null ? NotFound() : Ok(author);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AuthorRequestDto requestDto) 
        {
            var author = await _creator.Create<AuthorResponseDto>(requestDto);

            return Created($"api/authors/{author.Id}", author);
        }

        [SwaggerOperation(Summary = "Update a book author by unique id")]
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, AuthorRequestDto requestDto)
        {
            await _updater.Update(requestDto, id);
        
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
