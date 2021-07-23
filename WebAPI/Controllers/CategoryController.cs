using Core.Interfaces;
using Core.Requests;
using Core.ServiceResponses;
using Microsoft.AspNetCore.Mvc;
using Storage.Models;
using System.Net;
using System.Threading.Tasks;
using WebAPI.DTOs;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICrudService<Category> _crud;

        public CategoryController(ICrudService<Category> crud)
        {
            _crud = crud;
        }

        [HttpGet("{id:int}")]
        public async Task<ServiceResponse> GetCategory(int id)
        {
            var result = await _crud.GetById<CategoryDto>(id);

            return result.Content == null ? ServiceResponse.Error("Category not found.", HttpStatusCode.NotFound) : result;
        }

        [HttpGet]
        public async Task<ServiceResponse> GetCategories()
        {
            return await _crud.GetAll<CategoryDto>();        
        }

        [HttpPost]
        public async Task<ServiceResponse> Create(CategoryRequest category)
        {
            return await _crud.Create<CategoryDto>(category);
        }

        [HttpPut("{id:int}")]
        public async Task<ServiceResponse> Edit(int id, CategoryRequest category)
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
