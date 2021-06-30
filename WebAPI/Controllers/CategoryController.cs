using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Services;
using AutoMapper;
using WebAPI.DTOs;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetCategory(int id)
        {
            try
            {
                var result = await categoryService.GetCategory(id);
                if (result == null) return NotFound();
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        public async Task<ActionResult> GetCategories()
        {
            try
            {
                return Ok(await categoryService.GetCategories());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateCategoryDTO category)
        {
            try
            {
                if (category == null) return BadRequest();
                var newCategory = await categoryService.CreateCategory(category);
                return CreatedAtAction(nameof(GetCategory), new { id = newCategory.id }, newCategory);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Edit(CategoryDTO category)
        {
            try
            {
                if (categoryService.GetCategory(category.id).Result == null) return NotFound();
                var result = await categoryService.EditCategory(category);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var categoryToDelete = await categoryService.GetCategory(id);
                if (categoryToDelete == null) return NotFound();
                return Ok(await categoryService.DeleteCategory(id));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
