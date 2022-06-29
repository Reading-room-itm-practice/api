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
using System.Threading.Tasks;

namespace WebAPI.Controllers.Admin_Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminAuthorsController : ControllerBase
    {
        private readonly ICrudService<Author> _crud;
        private readonly IGettterPaginationService _getPaged;

        public AdminAuthorsController(ICrudService<Author> crud, IGettterPaginationService getPaged)
        {
            _crud = crud;
            _getPaged = getPaged;
        }

        [SwaggerOperation(Summary = "Retrieves all book authors")]
        [HttpGet]
        public async Task<ServiceResponse> Index([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            return await _getPaged.GetAll<Author, ApprovedAuthorDto>(filter, route, true);
        }

        [SwaggerOperation(Summary = "Retrieves a specific book author by unique id")]
        [HttpGet("{id:int}")]
        public async Task<ServiceResponse> Show(int id)
        {
            return await _crud.GetById<ApprovedAuthorDto>(id);
        }

        [HttpPost]
        public async Task<ServiceResponse> Create(ApproveAuthorRequest requestDto)
        {
            return await _crud.Create<ApprovedAuthorDto>(requestDto);
        }

        [SwaggerOperation(Summary = "Update a book author by unique id")]
        [HttpPut("{id:int}")]
        public async Task<ServiceResponse> Update(int id, ApproveAuthorRequest requestDto)
        {
            await _crud.Update(requestDto, id);

            return ServiceResponse.Success("Resource updated");
        }

        [SwaggerOperation(Summary = "Delete a book author by unique id")]
        [HttpDelete("{id:int}")]
        public async Task<ServiceResponse> Delete(int id)
        {
            await _crud.Delete(id);

            return ServiceResponse.Success("Resource deleted");
        }
    }
}