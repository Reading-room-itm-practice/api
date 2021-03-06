using Core.DTOs;
using Core.Interfaces;
using Core.Requests;
using Core.Response;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Storage.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ICreatorService<Book> _creator;
        private readonly IApprovedGetterService<Book> _getter;
        private readonly IGettterPaginationService _pagedGetter;

        public BooksController(ICreatorService<Book> creator, IApprovedGetterService<Book> getter, IGettterPaginationService pagedGetter)
        {
            _creator = creator;
            _getter = getter;
            _pagedGetter = pagedGetter;
        }

        [SwaggerOperation(Summary = "Retrieves all books")]
        [HttpGet]
        public async Task<ServiceResponse> Index([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            return await _pagedGetter.GetAll<Book, BookDto>(filter, route);
        }

        [SwaggerOperation(Summary = "Retrieves a specific book by unique id")]
        [HttpGet("{id:int}")]
        public async Task<ServiceResponse> Show(int id)
        {
           return await _getter.GetApprovedById<BookDto>(id);
        }

        [SwaggerOperation(Summary = "Creates a new entry of a book")]
        [HttpPost]
        public async Task<ServiceResponse> Create(BookRequest model)
        {
            return await _creator.Create<BookDto>(model);
        }
    }
}