using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Storage.Models;

namespace Core.Interfaces
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
