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
            var result = await categoryService.GetCategory(id);
            if (result == null) return NotFound();
            return Ok(result);
        }
        [HttpGet]
        public async Task<ActionResult> GetCategories()
        {
            return Ok(await categoryService.GetCategories());
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateCategoryDTO category)
        {
            if (category == null) return BadRequest();
            var newCategory = await categoryService.CreateCategory(category);
            return CreatedAtAction(nameof(GetCategory), new { id = newCategory.id }, newCategory);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Edit(int id, EditCategoryDTO category)
        {
            if (categoryService.GetCategory(id).Result == null) return NotFound();
            var result = await categoryService.EditCategory(id, category);
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var categoryToDelete = await categoryService.GetCategory(id);
            if (categoryToDelete == null) return NotFound();
            return Ok(await categoryService.DeleteCategory(id));
        }
    }
}
