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

        public BooksController(ICreatorService<Book> creator, IApprovedGetterService<Book> getter)
        {
            _creator = creator;
            _getter = getter;
        }

        [SwaggerOperation(Summary = "Retrieves all books")]
        [HttpGet("All")]
        public async Task<ServiceResponse> Index([FromQuery] PaginationFilter filter)
        {
           return await _getter.GetAllApproved<BookDto>();
        }

        [SwaggerOperation(Summary = "Retrieves specific book by unique id")]
        [HttpGet("{id:int}")]
        public async Task<ServiceResponse> Show(int id)
        {
           return await _getter.GetApprovedById<BookDto>(id);
        }

        [SwaggerOperation(Summary = "Create suggestion of book")]
        [HttpPost("Create")]
        public async Task<ServiceResponse> Create(BookRequest model)
        {
            return await _creator.Create<BookDto>(model);
        }
    }
}