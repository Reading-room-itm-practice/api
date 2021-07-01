using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DTOs;
using WebAPI.Models;

namespace WebAPI.Services
{
    public interface ICategoryService
    {
        public Task<IEnumerable<CategoryDTO>> GetCategories();
        public Task<CategoryDTO> CreateCategory(CreateCategoryDTO category);
        public Task<CategoryDTO> EditCategory(int id, EditCategoryDTO category);
        public Task<CategoryDTO> GetCategory(int id);
        public Task<CategoryDTO> DeleteCategory(int id);

    }
}
