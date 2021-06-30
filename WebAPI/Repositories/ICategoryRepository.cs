using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Repositories
{
    public interface ICategoryRepository
    {
        public Task<Category> CreateCategory(Category category);
        public Task<Category> DeleteCategory(int id);
        public Task<Category> EditCategory(Category category);
        public Task<IEnumerable<Category>> GetCategories();
        public  Task<Category> GetCategory(int id);
    }
}
