using Core.DTOs;
using Core.Interfaces;
using Core.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Storage.Identity;
using Storage.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    //[Authorize(Roles = UserRoles.Admin)]
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
            var authors = await _crud.GetAll<AuthorDto>();

            return Ok(authors);
        }

        [SwaggerOperation(Summary = "Retrieves a specific book author by unique id")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Show(int id)
        {
            var author = await _crud.GetById<AuthorDto>(id);

            return author == null ? NotFound() : Ok(author);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AuthorRequest requestDto)
        {
            var author = await _crud.Create<AuthorDto>(requestDto);

            return Created($"api/authors/{author.Id}", author);
        }

        [SwaggerOperation(Summary = "Update a book author by unique id")]
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, AuthorRequest requestDto)
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