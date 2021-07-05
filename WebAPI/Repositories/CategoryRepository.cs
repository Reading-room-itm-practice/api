using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DataAccessLayer;
using WebAPI.Models;

namespace WebAPI.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        
        private readonly ApiDbContext _context;
        public CategoryRepository(ApiDbContext apiDbContext)
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
            var result = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (result != null)
            {
                _context.Categories.Remove(result);
                await _context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<Category> EditCategory(Category category)
        {
            var result = await _context.Categories.FirstOrDefaultAsync(c => c.Id == category.Id);
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
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}

