using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetCategories();
        Task<Category> CreateCategory(Category category);
        Task<Category> EditCategory(Category category);
        Task<Category> GetCategory(int id);
        Task<Category> DeleteCategory(int id);

    }
}
