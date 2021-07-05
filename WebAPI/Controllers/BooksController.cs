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
    public class BooksController : ControllerBase
    {
        private readonly IGetterService<Book> _getter;
        private readonly ICreatorService<Book> _creator;
        private readonly IUpdaterService<Book> _updater;
        private readonly IDeleterService<Book> _deleter;

        public BooksController(ICreatorService<Book> creator, IGetterService<Book> getter, IUpdaterService<Book> updater, IDeleterService<Book> deleter)
        {
            _creator = creator;
            _getter = getter;
            _updater = updater;
            _deleter = deleter;
        }

        [SwaggerOperation(Summary = "Retrieves all books")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var books = await _getter.GetAll<BookResponseDto>();
            return Ok(books);
        }

        [SwaggerOperation(Summary = "Retrieves a specific book by unique id")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Show(int id)
        {
            var book = await _getter.GetById<BookResponseDto>(id);

            return book == null ? NotFound() : Ok(book);
        }

        [SwaggerOperation(Summary = "Creates a new entry of a book")]
        [HttpPost]
        public async Task<IActionResult> Create(BookRequestDto model)
        {
            var book = await _creator.Create<BookResponseDto>(model);

            return Created($"api/books/{book.Id}", book);
        }

        [SwaggerOperation(Summary = "Updates a book by unique id")]
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, BookRequestDto updateModel)
        {
            await _updater.Update(updateModel, id);
            return Ok("Resource updated");
        }

        [SwaggerOperation(Summary = "Deletes a book by unique id")]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _deleter.Delete(id);
            return Ok("Resource deleted");
        }
    }
}
