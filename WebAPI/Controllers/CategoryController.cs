using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebAPI.DTOs;
using WebAPI.Interfaces;
using Storage.Models;
using Core.Exceptions;
using Core.Requests;

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
        public async Task<ActionResult> GetCategory(int id)
        {
            var result = await _crud.GetById<CategoryDto>(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult> GetCategories()
        {
            var result = await _crud.GetAll<CategoryDto>();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CategoryRequest category)
        {
            try
            {
                var newCategory = await _crud.Create<CategoryDto>(category);
                return Created($"api/category/{newCategory.Id}", newCategory);
            }
            catch (ArgumentNullException e)
            {
                return BadRequest(e.Message);
                throw;
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Edit(int id, CategoryRequest category)
        {
            try
            {
                await _crud.Update(category, id);
                return Ok();
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _crud.Delete(id);
                return Ok();
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
