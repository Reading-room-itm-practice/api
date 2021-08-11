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
    public class AuthorsController : ControllerBase
    {
        private readonly ICreatorService<Author> _creator;
        private readonly IApprovedGetterService<Author> _getter;

        public AuthorsController(ICreatorService<Author> creator, IApprovedGetterService<Author> getter)
        {
            _creator = creator;
            _getter = getter;
        }

        [SwaggerOperation(Summary = "Retrieves all book authors")]
        [HttpGet]
        public async Task<ServiceResponse> Index([FromQuery] PaginationFilter filter)
        {
            return await _getter.GetAllApproved<AuthorDto>();
        }

        [SwaggerOperation(Summary = "Retrieves a specific book author by unique id")]
        [HttpGet("{id:int}")]
        public async Task<ServiceResponse> Show(int id)
        {
            return await _getter.GetApprovedById<AuthorDto>(id);
        }

        [HttpPost]
        public async Task<ServiceResponse> Create(AuthorRequest requestDto)
        {
            return await _creator.Create<AuthorDto>(requestDto);
        }
    }
}