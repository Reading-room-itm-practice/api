﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebAPI.DTOs;
using Core.Interfaces;
using Storage.Models;
using Core.Exceptions;
using Core.Requests;
using Core.ServiceResponses;
using System.Net;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

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

            return new SuccessResponse<CategoryDto>() { Message = "Category found.", Content = result };
        }

        [HttpGet]
        public async Task<ServiceResponse> GetCategories()
        {
            var result = await _crud.GetAll<CategoryDto>();
            return new SuccessResponse<IEnumerable<CategoryDto>>() { Message = "Categories retrieved.", Content = result };
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
            return new SuccessResponse() { Message = "Category updated." };
        }

        [HttpDelete("{id:int}")]
        public async Task<ServiceResponse> Delete(int id)
        {
            await _crud.Delete(id);
            return new SuccessResponse() { Message = "Category deleted." };
        }
    }
}
