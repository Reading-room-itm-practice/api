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
using WebAPI.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICreatorService<Category> _creator;
        private readonly IGetterService<Category> _getter;
        private readonly IUpdaterService<Category> _updater;
        private readonly IDeleterService<Category> _deleter;

        public CategoryController(ICreatorService<Category> creator, IGetterService<Category> getter, IUpdaterService<Category> updater, IDeleterService<Category> deleter)
        {
            _creator = creator;
            _getter = getter;
            _updater = updater;
            _deleter = deleter;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetCategory(int id)
        {
            var result = await _getter.GetById<CategoryResponseDto>(id);
            if (result == null) return NotFound();
            return Ok(result);
        }
        [HttpGet]
        public async Task<ActionResult> GetCategories()
        {
            var result = await _getter.GetAll<CategoryResponseDto>();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CategoryRequestDto category)
        {
            if (category == null) return BadRequest();
            var newCategory = await _creator.Create<CategoryResponseDto>(category);
            return Created($"api/category/{newCategory.Id}", newCategory);
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult> Edit(int id, CategoryRequestDto category)
        {
            if (_getter.GetById<CategoryResponseDto>(id).Result == null) return NotFound();
            await _updater.Update(category, id);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (_getter.GetById<CategoryResponseDto>(id) == null) return NotFound();
            await _deleter.Delete(id);
            return Ok();
        }
    }
}
