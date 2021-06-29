using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DataAccessLayer;
using WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApiDbContext _context;
        public CategoryService(ApiDbContext apiDbContext)
        {
            _context = apiDbContext;
        }

        public async Task<Category> CreateCategory(Category category)
        {
            var result = _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Category> DeleteCategory(int id)
        {
            var result = await _context.Categories.FirstOrDefaultAsync(c => c.id == id);
            if(result != null)
            {
                _context.Categories.Remove(result);
                await _context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<Category> EditCategory(Category category)
        {
            var result = await _context.Categories.FirstOrDefaultAsync(c => c.id == category.id);
            if (result != null)
            {
                result.Name = category.Name;
                await _context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategory(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.id == id);
        }
    }
}
