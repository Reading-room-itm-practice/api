using Core.DTOs;
using Core.Interfaces;
using Core.Requests;
using Core.Response;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Storage.Models;
using System.Net;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUserCrudService<Category> _crud;
        private readonly IGettterPaginationService _getPaged;

        public CategoryController(IUserCrudService<Category> crud, IGettterPaginationService getPaged)
        {
            _crud = crud;
            _getPaged = getPaged;
        }

        [HttpGet("{id:int}")]
        public async Task<ServiceResponse> GetCategory(int id)
        {
            var result = await _crud.GetById<CategoryDto>(id);

            return result.Content == null ? ServiceResponse.Error("Category not found.", HttpStatusCode.NotFound) : result;
        }

        [HttpGet]
        public async Task<ServiceResponse> GetCategories([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;

            return await _getPaged.GetAll<Category, CategoryDto>(filter, route);        
        }

        [HttpPost]
        public async Task<ServiceResponse> Create(CategoryRequest category)
        {
            return await _crud.Create<CategoryDto>(category);
        }
    }
}
