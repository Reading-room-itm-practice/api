using Core.DTOs;
using Core.Interfaces;
using Core.Requests;
using Core.Response;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Storage.Identity;
using Storage.Models;
using System.Net;
using System.Threading.Tasks;

namespace WebAPI.Controllers.Admin_Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminCategoryController : ControllerBase
    {
        private readonly ICrudService<Category> _crud;
        private readonly IGettterPaginationService _getPaged;

        public AdminCategoryController(ICrudService<Category> crud, IGettterPaginationService getPaged)
        {
            _crud = crud;
            _getPaged = getPaged;
        }

        [HttpGet("{id:int}")]
        public async Task<ServiceResponse> GetCategory(int id)
        {
            var result = await _crud.GetById<ApprovedCategoryDto>(id);

            return result.Content == null ? ServiceResponse.Error("Category not found.", HttpStatusCode.NotFound) : result;
        }

        [HttpGet]
        public async Task<ServiceResponse> GetCategories([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            return await _getPaged.GetAll<Category, ApprovedCategoryDto>(filter, route);
        }

        [HttpPost]
        public async Task<ServiceResponse> Create(ApproveCategoryRequest category)
        {
            return await _crud.Create<ApprovedCategoryDto>(category);
        }

        [HttpPut("{id:int}")]
        public async Task<ServiceResponse> Edit(int id, ApproveCategoryRequest category)
        {
            await _crud.Update(category, id);

            return ServiceResponse.Success("Category updated.");
        }

        [HttpDelete("{id:int}")]
        public async Task<ServiceResponse> Delete(int id)
        {
            await _crud.Delete(id);

            return ServiceResponse.Success("Category deleted.");
        }
    }
}
