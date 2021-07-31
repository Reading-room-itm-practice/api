using Core.DTOs;
using Core.Interfaces;
using Core.Requests;
using Core.Response;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Storage.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IUserCrudService<Book> _crud;

        public BooksController(IUserCrudService<Book> crud)
        {
            _crud = crud;
        }

        [SwaggerOperation(Summary = "Retrieves all books")]
        [HttpGet]
        public async Task<ServiceResponse> Index([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var books = await _crud.GetAll<BookDto>(filter, route);

            return books;
        }

        [SwaggerOperation(Summary = "Retrieves a specific book by unique id")]
        [HttpGet("{id:int}")]
        public async Task<ServiceResponse> Show(int id)
        {
            var book = await _crud.GetById<BookDto>(id);

            return book;
        }

        [SwaggerOperation(Summary = "Creates a new entry of a book")]
        [HttpPost]
        public async Task<ServiceResponse> Create(BookRequest model)
        {
            return await _crud.Create<BookDto>(model);
        }
    }
}