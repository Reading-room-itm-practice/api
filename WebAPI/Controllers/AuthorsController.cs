using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DTOs;
using WebAPI.Interfaces.Authors;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorCreator _creator;
        private readonly IAuthorGetter _getter;

        public AuthorsController(IAuthorCreator creator, IAuthorGetter getter)
        {
            _creator = creator;
            _getter = getter;
        }

        [SwaggerOperation(Summary = "Retrieves all book authors")]
        [HttpGet]
        public IActionResult Index()
        {
            var authors = _getter.GetAllAuthors();
            return Ok(authors);
        }

        [SwaggerOperation(Summary = "Retrieves a specific book author by unique id")]
        [HttpGet("{id}")]
        public IActionResult Show(int id)
        {
            var author = _getter.GetAuthorById(id);
            if (author == null)
            {
                return NotFound();
            }

            return Ok(author);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAuthorDto model) {
            var author = await _creator.AddAuthor(model);

            return Created($"api/authors/{author.Id}", author);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, EditCategoryDTO category)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
