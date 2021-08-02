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
    public class AdminBooksController : ControllerBase
    {
        private readonly ICrudService<Book> _crud;
        private readonly IGettterPaginationService _getPaged;

        public AdminBooksController(ICrudService<Book> crud, IGettterPaginationService getPaged)
        {
            _crud = crud;
            _getPaged = getPaged;
        }

        [SwaggerOperation(Summary = "Retrieves all books")]
        [HttpGet]
        public async Task<ServiceResponse> Index([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            return await _getPaged.GetAll<Book, ApprovedBookDto>(filter, route);
        }

        [SwaggerOperation(Summary = "Retrieves a specific book by unique id")]
        [HttpGet("{id:int}")]
        public async Task<ServiceResponse> Show(int id)
        {
            return await _crud.GetById<ApprovedBookDto>(id);
        }

        [SwaggerOperation(Summary = "Creates a new entry of a book")]
        [HttpPost]
        public async Task<ServiceResponse> Create(ApproveBookRequest model)
        {
            return await _crud.Create<ApprovedBookDto>(model);
        }

        [SwaggerOperation(Summary = "Updates a book by unique id")]
        [HttpPut("{id:int}")]
        public async Task<ServiceResponse> Update(int id, ApproveBookRequest updateModel)
        {
            await _crud.Update(updateModel, id);

            return ServiceResponse.Success("Resource updated");
        }

        [SwaggerOperation(Summary = "Deletes a book by unique id")]
        [HttpDelete("{id:int}")]
        public async Task<ServiceResponse> Delete(int id)
        {
            await _crud.Delete(id);

            return ServiceResponse.Success("Resource deleted");
        }
    }
}
