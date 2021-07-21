using Core.DTOs;
using Core.Interfaces;
using Core.Requests;
using Microsoft.AspNetCore.Mvc;
using Storage.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ICrudService<Book> _crud;

        public BooksController(ICrudService<Book> crud)
        {
            _crud = crud;
        }

        [SwaggerOperation(Summary = "Retrieves all books")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var books = await _crud.GetAll<BookDto>();
            return Ok(books);
        }

        [SwaggerOperation(Summary = "Retrieves a specific book by unique id")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Show(int id)
        {
            var book = await _crud.GetById<BookDto>(id);

            return book == null ? NotFound() : Ok(book);
        }

        [SwaggerOperation(Summary = "Creates a new entry of a book")]
        [HttpPost]
        public async Task<IActionResult> Create(BookRequest model)
        {
            var book = await _crud.Create<BookDto>(model);
            return Created($"api/books/{book.Id}", book);
        }

        [SwaggerOperation(Summary = "Updates a book by unique id")]
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, BookRequest updateModel)
        {
            await _crud.Update(updateModel, id);
            return Ok("Resource updated");
        }

        [SwaggerOperation(Summary = "Deletes a book by unique id")]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _crud.Delete(id);
            return Ok("Resource deleted");
        }
    }
}