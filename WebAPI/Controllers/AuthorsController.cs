using Core.DTOs;
using Core.Interfaces;
using Core.Requests;
using Core.ServiceResponses;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Storage.Identity;
using Storage.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IUserCrudService<Author> _crud;

        public AuthorsController(IUserCrudService<Author> crud)
        {
            _crud = crud;
        }

        [SwaggerOperation(Summary = "Retrieves all book authors")]
        [HttpGet]
        public async Task<ServiceResponse> Index()
        {
            return await _crud.GetAll<AuthorDto>();
        }

        [SwaggerOperation(Summary = "Retrieves a specific book author by unique id")]
        [HttpGet("{id:int}")]
        public async Task<ServiceResponse> Show(int id)
        {
            return await _crud.GetById<AuthorDto>(id);
        }

        [HttpPost]
        public async Task<ServiceResponse> Create(AuthorRequest requestDto)
        {
            return await _crud.Create<AuthorDto>(requestDto);
        }
    }
}