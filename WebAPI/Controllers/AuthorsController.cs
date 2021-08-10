using Core.DTOs;
using Core.Interfaces;
using Core.Requests;
using Core.Response;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Storage.Identity;
using Storage.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IUserCrudService<Author> _crud;
        private readonly IGettterPaginationService _getPaged;

        public AuthorsController(IUserCrudService<Author> crud, IGettterPaginationService getPaged)
        {
            _crud = crud;
            _getPaged = getPaged;
        }

        [SwaggerOperation(Summary = "Retrieves all book authors")]
        [HttpGet("All")]
        public async Task<ServiceResponse> Index([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var authors = await _getPaged.GetAll<Author, AuthorDto>(filter, route);

            return authors;
        }

        [SwaggerOperation(Summary = "Retrieves a specific book author by unique id")]
        [HttpGet("{id:int}")]
        public async Task<ServiceResponse> Show(int id)
        {
            return await _crud.GetById<AuthorDto>(id);
        }

        [SwaggerOperation(Summary = "Create suggestion of book author")]
        [HttpPost("Create")]
        public async Task<ServiceResponse> Create(AuthorRequest requestDto)
        {
            return await _crud.Create<AuthorDto>(requestDto);
        }
    }
}