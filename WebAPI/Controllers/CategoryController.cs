using Core.Interfaces;
using Core.Requests;
using Core.Response;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Storage.Models;
using System.Collections.Generic;
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
            if (result == null) return new SuccessResponse() { Message = "Category not found.", StatusCode = HttpStatusCode.OK };
            return new SuccessResponse<CategoryDto>() { Message = "Category found.", StatusCode = HttpStatusCode.OK, Content = result };
        }

        [HttpGet]
        public async Task<ServiceResponse> GetCategories([FromQuery] PaginationFilter filter)
        {
            var result = await _crud.GetAll<CategoryDto>(filter);
            return new SuccessResponse<IEnumerable<CategoryDto>>() { Message = "Categories retrieved.", StatusCode = HttpStatusCode.OK, Content = result };
        }

        [HttpPost]
        public async Task<ServiceResponse> Create(CategoryRequest category)
        {
            var newCategory = await _crud.Create<CategoryDto>(category);
            return new SuccessResponse<CategoryDto>()
            { Message = "Category created.", StatusCode = HttpStatusCode.Created, Content = newCategory };
        }

        [HttpPut("{id:int}")]
        public async Task<ServiceResponse> Edit(int id, CategoryRequest category)
        {
            await _crud.Update(category, id);
            return new SuccessResponse()
            { Message = "Category updated.", StatusCode = HttpStatusCode.OK };
        }

        [HttpDelete("{id:int}")]
        public async Task<ServiceResponse> Delete(int id)
        {
            await _crud.Delete(id);
            return new SuccessResponse()
            { Message = "Category deleted.", StatusCode = HttpStatusCode.OK };
        }
    }
}
