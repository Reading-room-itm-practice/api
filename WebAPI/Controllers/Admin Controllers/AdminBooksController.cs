using Core.DTOs;
using Core.Interfaces;
using Core.Requests;
using Core.ServiceResponses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Storage.Identity;
using Storage.Models;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers.Admin_Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminBooksController : ControllerBase
    {
        private readonly ICrudService<Book> _crud;

        public AdminBooksController(ICrudService<Book> crud)
        {
            _crud = crud;
        }

        [SwaggerOperation(Summary = "Retrieves all books")]
        [HttpGet]
        public async Task<ServiceResponse> Index()
        {
            return await _crud.GetAll<ApprovedBookDto>();
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
