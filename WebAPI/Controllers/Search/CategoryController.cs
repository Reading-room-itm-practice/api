using Core.DTOs;
using Core.Interfaces;
using Core.Requests;
using Core.Response;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Storage.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICreatorService<Category> _creator;
        private readonly IApprovedGetterService<Category> _getter;

        public CategoryController(ICreatorService<Category> creator, IApprovedGetterService<Category> getter)
        {
            _creator = creator;
            _getter = getter;
        }

        [SwaggerOperation(Summary = "Retrieves all categories")]
        [HttpGet("All")]
        public async Task<ServiceResponse> GetCategories([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;

            return await _getter.GetAllApproved<CategoryDto>();
        }

        [SwaggerOperation(Summary = "Retrieves specific category by unique id")]
        [HttpGet("{id:int}")]
        public async Task<ServiceResponse> GetCategory(int id)
        {
            var result = await _getter.GetApprovedById<CategoryDto>(id);

            return result.Content == null ? ServiceResponse.Error("Category not found.", HttpStatusCode.NotFound) : result;
        }

        [SwaggerOperation(Summary = "Create suggestion of category")]
        [HttpPost("Create")]
        public async Task<ServiceResponse> Create(CategoryRequest category)
        {
            return await _creator.Create<CategoryDto>(category);
        }
    }
}
