using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DataAccessLayer;
using WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using WebAPI.DTOs;
using WebAPI.Repositories;
using AutoMapper;

namespace WebAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CategoryDTO> CreateCategory(CreateCategoryDTO category)
        {
            var result = _mapper.Map<Category>(category);
            await _categoryRepository.CreateCategory(result);
            return _mapper.Map<CategoryDTO>(result);
        }

        public async Task<CategoryDTO> DeleteCategory(int id)
        {
            var result = await _categoryRepository.DeleteCategory(id);
            return _mapper.Map<CategoryDTO>(result);
        }

        public async Task<CategoryDTO> EditCategory(int id, EditCategoryDTO category)
        {
            var result = _mapper.Map<Category>(category);
            result.Id = id;
            await _categoryRepository.EditCategory(result);
            return _mapper.Map<CategoryDTO>(result);
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategories()
        {
            var result = await _categoryRepository.GetCategories();
            return _mapper.Map<IEnumerable<CategoryDTO>>(result);
        }

        public async Task<CategoryDTO> GetCategory(int id)
        {
            var result = await _categoryRepository.GetCategory(id);
            return _mapper.Map<CategoryDTO>(result);
        }
    }
}
