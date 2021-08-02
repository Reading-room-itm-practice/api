using Core.DTOs;
using Core.Interfaces;
using Core.Requests;
using Core.ServiceResponses;
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
        private readonly ICreatorService<Category> _creator;
        private readonly IApprovedGetterService<Category> _getter;

        public CategoryController(ICreatorService<Category> creator, IApprovedGetterService<Category> getter)
        {
            _creator = creator;
            _getter = getter;
        }

        [HttpGet]
        public async Task<ServiceResponse> GetCategories()
        {
            return await _getter.GetAllApproved<CategoryDto>();
        }

        [HttpGet("{id:int}")]
        public async Task<ServiceResponse> GetCategory(int id)
        {
            var result = await _getter.GetApprovedById<CategoryDto>(id);

            return result.Content == null ? ServiceResponse.Error("Category not found.", HttpStatusCode.NotFound) : result;
        }

        [HttpPost]
        public async Task<ServiceResponse> Create(CategoryRequest category)
        {
            return await _creator.Create<CategoryDto>(category);
        }
    }
}
