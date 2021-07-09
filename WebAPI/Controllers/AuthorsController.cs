using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DTOs;
using Core.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using Storage.Models;

namespace WebAPI.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly ICrudService<Author> _crud;
        public AuthorsController(ICrudService<Author> crud)
        {
            _crud = crud;
        }

        [SwaggerOperation(Summary = "Retrieves all book authors")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var authors = await _crud.GetAll<AuthorResponseDto>();

            return Ok(authors);
        }

        [SwaggerOperation(Summary = "Retrieves a specific book author by unique id")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Show(int id)
        {
            var author = await _crud.GetById<AuthorResponseDto>(id);

            return author == null ? NotFound() : Ok(author);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AuthorRequestDto requestDto)
        {
            var author = await _crud.Create<AuthorResponseDto>(requestDto);

            return Created($"api/authors/{author.Id}", author);
        }

        [SwaggerOperation(Summary = "Update a book author by unique id")]
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, AuthorRequestDto requestDto)
        {
            await _crud.Update(requestDto, id);

            return Ok("Resource updated");
        }

        [SwaggerOperation(Summary = "Delete a book author by unique id")]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _crud.Delete(id);

            return Ok("Resource deleted");
        }
    }
}